using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace App;

public class CsvBuilder<T>
{
    class SetupInfo
    {
        public string HeaderText { get; set; } = string.Empty;
        public IValueFormatter Formatter { get; set; }
    }

    private readonly Dictionary<string, SetupInfo> _setupDic = new();
    private readonly IEnumerable<T> _data;

    public CsvBuilder(IEnumerable<T> data)
    {
        _data = data;
    }

    public CsvBuilder<T> Setup<TProperty>(Expression<Func<T, TProperty>> expression, string? customColumnName = null,
        Expression<Func<TProperty, string>>? formatExpression = null)
    {
        if (expression.Body is not MemberExpression memberExpression)
            throw new Exception("Invalid Expression");

        var selectedPropertyName = memberExpression.Member.Name;

        Func<TProperty, string> formatFunc = formatExpression != null ?
            formatExpression.Compile() : ((v) => v?.ToString() ?? string.Empty);

        var properties = PropertyInfoCache.GetCached(typeof(T));
        var propInfo = properties.First(p => p.Name == selectedPropertyName);

        _setupDic.Add(selectedPropertyName, new SetupInfo
        {
            HeaderText = string.IsNullOrWhiteSpace(customColumnName) ? selectedPropertyName : customColumnName,
            Formatter = new ValueReader<TProperty>(propInfo, formatFunc)
        });

        return this;
    }

    public byte[] ToBytes()
    {
        var sb = new StringBuilder();
        sb.AppendJoin(",", _setupDic.Values.Select(t => $"\"{t.HeaderText}\""));
        foreach (var d in _data)
        {
            sb.AppendLine();
            sb.AppendJoin(",", _setupDic.Values.Select(t => t.Formatter.Read(d)));
        }

        return Encoding.UTF8.GetBytes(sb.ToString());
    }
}
