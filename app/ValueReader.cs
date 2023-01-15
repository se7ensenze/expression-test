using System.Reflection;

namespace App;

public class ValueReader<TProperty> : IValueReader
{
    private readonly PropertyInfo _propInfo;
    private readonly Func<TProperty, string> _formatFunc;

    public ValueReader(PropertyInfo propInfo, Func<TProperty, string> formatFunc)
    {
        _propInfo = propInfo;
        _formatFunc = formatFunc;
    }

    public string Read(object? instance)
    {
        if (instance is null) return string.Empty;

        var value = _propInfo.GetValue(instance, null);

        if (value is null) return string.Empty;

        var text = _formatFunc.Invoke((TProperty)value);

        if (text.Contains('"'))
        {
            text = text.Replace("\"", "\"\"");
        }

        return $"\"{text}\"";
    }
}