using System.Text.Json.Nodes;

namespace ApiTests
{
    static class HelperExt
    {
        public static string? Value(this JsonNode node, string name)
        {
            var value = node[name];
            return value?.ToString();
        }
    }
}