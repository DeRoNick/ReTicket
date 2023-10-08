namespace ReTicket.Application.Rules
{
    public class PriceRuleOptions
    {
        public const string PriceRule = "PriceRule";

        public int MaximumMarginPercentage { get; set; }
        public int MarginCommissionPercentage { get; set; }
    }
}
