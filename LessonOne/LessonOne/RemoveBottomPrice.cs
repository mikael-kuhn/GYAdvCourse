namespace LessonOne
{
    public class RemoveSellingPoint : IMessage
    {
        public double Price { get; set; }

        public RemoveSellingPoint(double price)
        {
            Price = price;
        }
    }
}