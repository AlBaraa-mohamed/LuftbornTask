namespace LuftbornTask.src.WebUI.Strategies
{
    public class RegularPriceStrategy : IPriceStrategy
    {
        public decimal CalculatePrice(decimal basePrice) => basePrice;
    }
}
