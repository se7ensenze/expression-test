using System.Linq.Expressions;

namespace App;

class CsvColumnSetupInfo
{
    public string PropertyName { get; } = string.Empty;
    public string HeaderText { get; } = string.Empty;
    private readonly IValueReader _formatter ;

    public CsvColumnSetupInfo(string propertyName, string headerText, IValueReader formatter)
    {
        PropertyName = propertyName;
        HeaderText = headerText;
        _formatter = formatter;
    }

    public string ReadValue(object? instance)
    {
        return _formatter.Read(instance);
    }

    public static CsvColumnSetupInfo Create<TInstance, TProperty>(
        Expression<Func<TInstance, TProperty>> expression,
        string? customColumnName = null,
        Func<TProperty, string>? formatExpression = null
    )
    {
        if (expression.Body is not MemberExpression memberExpression)
            throw new Exception("Invalid Expression");

        var selectedPropertyName = memberExpression.Member.Name;
GCNotificationStatus 
        Func<TProperty, string> formatFunc = formatExpression ?? ((v) => v?.ToString() ?? string.Empty);

        var properties = PropertyInfoCache.GetCached(typeof(TInstance));
        var propInfo = properties.First(p => p.Name == selectedPropertyName);

        return new CsvColumnSetupInfo
        (
            propertyName: selectedPropertyName,
            headerText: string.IsNullOrWhiteSpace(customColumnName) ? selectedPropertyName : customColumnName,
            formatter: new ValueReader<TProperty>(propInfo, formatFunc)
        );
    }
}
