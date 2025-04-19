namespace LuftbornTask.src.WebUI.Strategies
{
    public class PremiumPriceStrategy : IPriceStrategy
    {
        public decimal CalculatePrice(decimal basePrice) => basePrice * 1.25m; // 25% markup
    }
}
