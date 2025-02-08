using Api.Models;

namespace Api.Configs
{
    public class PaycheckConfig
    {
        public const string SectionName = "PaycheckConfig";

        public PayFrequency PayFrequency { get; set; }
    }
}
