namespace App;

static class CsvExtensions
{
    public static string ToCsvRow(this Foo foo)
        => $"{foo.Name.ToStringCsvExcape()},{foo.Value.ToStringCsvExcape()}";

    public static string ToStringCsvExcape(this object value)
        => $"\"{(value.ToString() ?? string.Empty).Replace("\"", "\"\"")}\"";
}