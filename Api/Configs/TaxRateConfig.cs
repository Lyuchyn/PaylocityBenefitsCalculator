using System.ComponentModel.DataAnnotations;

namespace Api.Configs
{
    public class TaxRateConfig
    {
        public const string SectionName = "TaxRateConfig";

        public List<TaxRateBracket> Brackets { get; set; } = new List<TaxRateBracket>();
    }

    public class TaxRateBracket
    {
        [Range(0, 100)]
        public decimal TaxRatePercent { get; set; }

        [Range(0, double.MaxValue)]
        public decimal IncomeFrom { get; set; }

        [Range(0, double.MaxValue)]
        public decimal IncomeTo { get; set; }
    }
}
