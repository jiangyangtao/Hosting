using System.Collections;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Mvc.FormatResult
{
    public class TextValueOptionCollection : IEnumerable<TextValueOption>
    {
        private readonly List<TextValueOption> Options;

        public TextValueOptionCollection()
        {
            Options = new List<TextValueOption>();
        }

        public TextValueOptionCollection(IEnumerable<TextValueOption> options) : this()
        {
            if (options.NotNullAndEmpty()) Options.AddRange(options);
        }

        public static IEnumerable<TextValueOption> Empty => Array.Empty<TextValueOption>();

        public void Add(TextValueOption option) => Options.Add(option);

        public IEnumerator<TextValueOption> GetEnumerator() => Options.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
