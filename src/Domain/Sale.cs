namespace Domain
{
    public class Sale
    {
        public Product Product { get; }
        public int[] Change { get; }

        public Sale(Product product, int[] change)
        {
            Product = product;
            Change = change;
        }
    }
}