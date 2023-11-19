namespace Yangtao.Hosting.Mvc.FormatResult
{
    public class TextValueOption
    {
        public TextValueOption(string value, string text, string description = "")
        {
            Value = value;
            Text = text;
            Description = description;
        }

        public string Value { set; get; }

        public string Text { set; get; }

        public string Description { set; get; }
    }
}
