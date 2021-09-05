namespace O10.Nomy.DemoFeatures
{
    public record ValidationEntryDTO
    {
        public string Key { get; set; }
        public string Prompt { get; set; }
        public bool Value { get; set; }
    }
}
