namespace Api.Configs
{
    public class TaxRateConfig
    {
        public const string SectionName = "TaxRateConfig";

        public List<TaxRateBracket> Brackets { get; set; } = new List<TaxRateBracket>();
    }

    public class TaxRateBracket
    {
        public decimal TaxRatePercent { get; set; }

        public decimal IncomeFrom { get; set; }

        public decimal IncomeTo { get; set; }
    }
}
