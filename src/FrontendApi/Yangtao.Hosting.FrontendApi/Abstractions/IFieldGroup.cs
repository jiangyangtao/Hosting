namespace Yangtao.Hosting.FrontendApi.Abstractions
{
    internal interface IFieldGroup
    {
        public string? GroupName { get; }

        public int SortIndex {  get; }
    }
}
