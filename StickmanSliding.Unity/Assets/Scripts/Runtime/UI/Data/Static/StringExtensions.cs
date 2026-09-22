using System.Linq;

namespace StickmanSliding.UI.Data.Static
{
    public static class StringExtensions
    {
        private const char Dash = '-';

        public static string ToKebabCase(this string source) =>
            string.Concat(source.Select((character, index) =>
                (char.IsUpper(character) && index > 0 ? Dash.ToString() : string.Empty) + char.ToLower(character)));
    }
}