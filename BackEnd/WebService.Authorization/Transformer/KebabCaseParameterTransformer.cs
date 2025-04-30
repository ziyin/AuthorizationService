using System.Text.RegularExpressions;

namespace WebService.Authorization.HttpApi.Host.Transformer;

public partial class KebabCaseParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        var valueString = value?.ToString();
        return string.IsNullOrWhiteSpace(valueString) ? null : MyRegex().Replace(valueString, "$1-$2").ToLower();
    }

    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex MyRegex();
}