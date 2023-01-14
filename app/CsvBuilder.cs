using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace App;
public class CsvBuilder<T>
{
    private readonly Dictionary<string, CsvColumnSetupInfo> _setupDic = new();
    private readonly IEnumerable<T> _data;

    public CsvBuilder(IEnumerable<T> data)
    {
        _data = data;
    }

    public CsvBuilder<T> Setup<TProperty>(Expression<Func<T, TProperty>> expression, string? customColumnName = null,
        Func<TProperty, string>? formatExpression = null)
    {
        var newSetup = CsvColumnSetupInfo.Create<T, TProperty>(expression, customColumnName, formatExpression);

        _setupDic.Add(newSetup.PropertyName, newSetup);

        return this;
    }

    public byte[] ToBytes()
    {
        var sb = new StringBuilder();
        sb.AppendJoin(",", _setupDic.Values.Select(t => $"\"{t.HeaderText}\""));
        foreach (var d in _data)
        {
            sb.AppendLine();
            sb.AppendJoin(",", _setupDic.Values.Select(setup => setup.ReadValue(d)));
        }

        return Encoding.UTF8.GetBytes(sb.ToString());
    }
}
