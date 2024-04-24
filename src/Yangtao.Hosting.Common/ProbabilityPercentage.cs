namespace Yangtao.Hosting.Common
{
    /// <summary>
    /// 概率百分比
    /// </summary>
    public class ProbabilityPercentage
    {
        private readonly int Percentage;

        private ProbabilityPercentage(int percentage)
        {
            Percentage = percentage;
        }

        public static ProbabilityPercentage Create(int percentage) => new(percentage);

        public bool IsHit => RandomNumber <= Percentage;

        private static int RandomNumber
        {
            get
            {
                var random = new Random();
                return random.Next(0, 100);
            }
        }
    }
}
