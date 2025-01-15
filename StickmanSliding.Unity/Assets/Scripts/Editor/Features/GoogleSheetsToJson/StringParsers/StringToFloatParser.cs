namespace StickmanSliding.Editor.Features.GoogleSheetsToJson.StringParsers
{
    public class StringToFloatParser : IStringParser<float>
    {
        public float Parse(string input) => string.IsNullOrEmpty(input) ? 0 : float.Parse(input);
    }
}