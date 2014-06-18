namespace Restaurant
{
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Xml;

using Newtonsoft.Json.Linq;

using Restaurant.Events;

    public class AtomDispatcher
    {
        private static SyndicationLink GetNamedLink(IEnumerable<SyndicationLink> links, string name)
        {
            return links.FirstOrDefault(link => link.RelationshipType == name);
        }

        static Uri GetLast(Uri head)
        {
            var request = (HttpWebRequest)WebRequest.Create(head);
            request.Credentials = new NetworkCredential("admin", "changeit");
            request.Accept = "application/atom+xml";
            try
            {
                using (var response = (HttpWebResponse) request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                        return null;
                    using (var xmlreader =XmlReader.Create(response.GetResponseStream()))
                    {
                        var feed = SyndicationFeed.Load(xmlreader);
                        var last = GetNamedLink(feed.Links, "last");
                        return (last != null) ? last.Uri : GetNamedLink(feed.Links, "self").Uri;
                    }
                }
            }
            catch(WebException ex)
            {
                if (((HttpWebResponse) ex.Response).StatusCode == HttpStatusCode.NotFound) return null;
                throw;
            }
        }

        private static void ProcessItem(SyndicationItem item)
        {
            Console.WriteLine(item.Title.Text);
            //get events
            var request = (HttpWebRequest)WebRequest.Create(GetNamedLink(item.Links, "alternate").Uri);
            request.Credentials = new NetworkCredential("admin", "changeit");
            request.Accept = "application/json";

            using (var response = request.GetResponse())
            {
                var streamReader = new StreamReader(response.GetResponseStream());
                // Console.WriteLine(streamReader.ReadToEnd());
                var orderText = streamReader.ReadToEnd();
                Order order = new Order(orderText);
                string eventType = item.Summary.Text;
                Type type = Assembly.GetExecutingAssembly().GetTypes().First(t => t.Name == eventType);
                object instance = Activator.CreateInstance(type, order);
                Dispatcher.Instance.Publish((IEvent)instance);
            }
        }

        private Uri ReadPrevious(Uri uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Credentials = new NetworkCredential("admin", "changeit");
            request.Accept = "application/atom+xml";
            using(var response = request.GetResponse())
            {
                using(var xmlreader = XmlReader.Create(response.GetResponseStream()))
                {
                    var feed = SyndicationFeed.Load(xmlreader);
                    foreach (var item in feed.Items.Reverse())
                    {
                        ProcessItem(item);
                    }
                    var prev = GetNamedLink(feed.Links, "previous");
                    return prev == null ? uri : prev.Uri;
                }
            }
        }

        public void PostMessage(IEvent @event, string stream)
        {
            var message = string.Format(
                "[{{\"eventType\":\"{0}\", \"eventId\" :\"{1}\", \"data\" : {2}}}]",
                @event.GetType().Name,
                @event.Id,
                ((OrderEvent)@event).Order);
            var request = WebRequest.Create("http://192.168.30.122:2113/streams/" + stream);
            request.Method = "POST";
            request.ContentType = "application/vnd.eventstore.events+json";
            request.ContentLength = message.Length;
            using(var sw= new StreamWriter(request.GetRequestStream()))
            {
                sw.Write(message);
            }
            using(var response = request.GetResponse())
            {
                response.Close();
            }
        }

        private void Pull(string stream)
        {
            Uri last = null;
            var stop = false;
            while (last == null && !stop)
            {
                last = GetLast(new Uri("http://192.168.30.122:2113/streams/" + stream));
                if(last == null) Thread.Sleep(1000);
                if (Console.KeyAvailable)
                {
                    stop = true;
                }
            }

            while(!stop)
            {
                var current = ReadPrevious(last);
                if(last == current)
                {
                    Thread.Sleep(1000);
                }
                last = current;
                if(Console.KeyAvailable)
                {
                    stop = true;
                }
            }
        }

        public void Start(string stream)
        {
            var thread = new Thread(() => Pull(stream));
            thread.Start();
        }
    }
}
