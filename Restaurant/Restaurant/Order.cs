using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Restaurant
{
    using Newtonsoft.Json.Linq;

    public class Order
    {
        private readonly JObject innerInstance;

        public Order(string json)
        {
            var d1 = DateTime.Now;
            TimeToLive = d1.Add(TimeSpan.FromSeconds(5));
            if (string.IsNullOrEmpty(json))
            {
                innerInstance = new JObject();
                Id = Guid.NewGuid().ToString();
            }
            else
            {
                innerInstance = JObject.Parse(json);
            }
            if (innerInstance["lines"] == null)
            {
                innerInstance.Add("lines", new JArray());
            }
            lines = innerInstance["lines"].Select(x => new Line(x.ToString())).ToList();
        }

        public override string ToString()
        {
            return innerInstance.ToString();
        }

        public bool IsDodgy 
        {
            get
            {
                return innerInstance["isDodgy"] != null && bool.Parse(innerInstance["isDodgy"].ToString());
            }
            set
            {
                innerInstance["isDodgy"] = value.ToString();
            }
        }

        public string Id 
        {
            get
            {
                return innerInstance["id"].ToString();
            }
            set
            {
                innerInstance["id"] = value;
            }
        }

        public string Waiter 
        {
            get
            {
                return innerInstance["waiter"].ToString();
            }
            set
            {
                innerInstance["waiter"] = value;
            }
        }

        public string Cook
        {
            get
            {
                return innerInstance["cook"].ToString();
            }
            set
            {
                innerInstance["cook"] = value;
            }
        }

        public int Table
        {
            get
            {
                return int.Parse(innerInstance.Property("table").Value.ToString());
            }
            set
            {
                innerInstance["table"] = value;
            }
        }

        public int CookTime
        {
            get
            {
                return int.Parse(innerInstance.Property("cookTime").Value.ToString());
            }
            set
            {
                innerInstance["cookTime"] = value;
            }
        }

        public double Tax
        {
            get
            {
                return double.Parse(innerInstance.Property("tax").Value.ToString());
            }
            set
            {
                innerInstance["tax"] = value;
            }
        }

        public double SubTotal
        {
            get
            {
                return double.Parse(innerInstance.Property("subTotal").Value.ToString());
            }
            set
            {
                innerInstance["subTotal"] = value;
            }
        }

        public double Total
        {
            get
            {
                return double.Parse(innerInstance.Property("total").Value.ToString());
            }
            set
            {
                innerInstance["total"] = value;
            }
        }

        public string Card
        {
            get
            {
                return innerInstance.Property("card").Value.ToString();
            }
            set
            {
                innerInstance["card"] = value;
            }
        }

        private List<Line> lines;
        public IEnumerable<Line> Lines
        {
            get
            {
                return lines;
            }
        }

        public Line AddLine(string name, string ingredients, double price)
        {
            Line newLine = new Line(name, ingredients, price);
            AddLine(newLine);
            return newLine;
        }

        public void AddLine(Line line)
        {
            lines.Add(line);
            innerInstance["lines"] = new JArray(JsonConvert.SerializeObject(lines));
        }

        public DateTime TimeToLive
        {
            get;
            set;
        }
    }
}
