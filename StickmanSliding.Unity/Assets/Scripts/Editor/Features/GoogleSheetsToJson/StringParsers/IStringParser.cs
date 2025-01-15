using StickmanSliding.Editor.Utilities;

namespace StickmanSliding.Editor.Features.GoogleSheetsToJson.StringParsers
{
    public interface IStringParser
    {
        string Type { get; }

        object Parse(string input);
    }

    public interface IStringParser<out T> : IStringParser
    {
        string IStringParser.Type => typeof(T).AliasOrName();

        object IStringParser.Parse(string input) => Parse(input);

        new T Parse(string input);
    }
}