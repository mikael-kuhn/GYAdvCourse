
namespace Restaurant
{
    using Newtonsoft.Json.Linq;

    public sealed class Line
    {
        private readonly JObject line;

        public Line(string json)
        {
            line = JObject.Parse(json);
        }

        public Line(string name, string ingredients, double price)
        {
            line = new JObject { { "name", name }, { "ingredients", ingredients }, { "price", price } };
        }

        public string Name 
        {
            get
            {
                return line.Property("name").Value.ToString();
            }
            set
            {
                line.Property("name").Value = value;
            }
        }

        public string Ingredients
        {
            get
            {
                return line.Property("ingredients").Value.ToString();
            }
            set
            {
                line.Property("ingredients").Value = value;
            }
        }

        public double Price
        {
            get
            {
                return double.Parse(line.Property("price").Value.ToString());
            }
            set
            {
                line.Property("price").Value = value;
            }
        }

        public override string ToString()
        {
            return line.ToString();
        }
    }
}
