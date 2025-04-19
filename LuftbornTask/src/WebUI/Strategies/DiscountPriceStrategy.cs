namespace LuftbornTask.src.WebUI.Strategies
{
    public class DiscountPriceStrategy : IPriceStrategy
    {
        public decimal CalculatePrice(decimal basePrice) => basePrice * 0.9m; // 10% discount
    }
}
