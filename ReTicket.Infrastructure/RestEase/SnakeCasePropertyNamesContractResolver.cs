using System.Text;
using Newtonsoft.Json.Serialization;

namespace ReTicket.Infrastructure.RestEase;

public class SnakeCasePropertyNamesContractResolver : DefaultContractResolver
{
    protected override string ResolvePropertyName(string propertyName)
    {
        return ConvertToSnakeCase(propertyName);
    }

    private static string ConvertToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var builder = new StringBuilder();
        builder.Append(char.ToLower(input[0]));

        for (int i = 1; i < input.Length; i++)
        {
            if (char.IsUpper(input[i]))
            {
                builder.Append('_');
                builder.Append(char.ToLower(input[i]));
            }
            else
            {
                builder.Append(input[i]);
            }
        }

        return builder.ToString();
    }
}