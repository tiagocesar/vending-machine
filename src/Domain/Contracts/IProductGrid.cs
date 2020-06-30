namespace Domain.Contracts
{
    public interface IProductGrid
    {
        Product AddProduct(int code, string name, int price, int quantity);
        Product GetProduct(int code);
    }
}