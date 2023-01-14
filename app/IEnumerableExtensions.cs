namespace App;

public static class IEnumerableExtensions
{
    public static CsvBuilder<T> CreateCsvBuilder<T>(this IEnumerable<T> data)
    {
        return new CsvBuilder<T>(data);
    }
}
