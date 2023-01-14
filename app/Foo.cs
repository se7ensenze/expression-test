using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace App;

class Foo
{
    public int Value { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<string> Sentences = new();
}

// static class FooExtensions
// {
//     public static Foo SaySomething(this Foo foo, string sentence)
//     {
//         foo.Sentences.Add(sentence);
//         return foo;
//     }
// }

// interface ICsvColumn
// {
//     string PropertyName { get; }
//     string Header { get; }
//     string ReadValue<T>(T instance);
// }

// interface ICsvColumnSetup<TProperty>
// {
//     ICsvColumnSetup<TProperty> SetHeader(string newHeader);
//     ICsvColumnSetup<TProperty> SetFormat(Expression<Func<TProperty, string>> expression);
// }

// interface IObjectFormatter
// {
//     string Format(object value);
// }

// class ValueFormatter<TValue> : IObjectFormatter
// {
//     private readonly Func<TValue, string> _format;

//     public ValueFormatter(Func<TValue, string> format)
//     {
//         _format = format;
//     }

//     public string Format(object value)
//     {
//         return _format.Invoke((TValue)value);
//     }
// }

// class CsvColumnSetup<TProperty> : ICsvColumnSetup<TProperty>

// {
//     public ICsvColumnSetup<TProperty> SetFormat(Expression<Func<TProperty, string>> expression)
//     {
//         throw new NotImplementedException();
//     }

//     public ICsvColumnSetup<TProperty> SetHeader(string newHeader)
//     {
//         throw new NotImplementedException();
//     }
// }

// class CsvColumn<TProperty> : ICsvColumn
// {
//     public string PropertyName { get; }
//     private string _header;
//     private IObjectFormatter? _customFormat;
//     private readonly PropertyInfo _propertyInfo;
//     private CsvColumnSetup<TProperty>

//     public CsvColumn(PropertyInfo propertyInfo)
//     {
//         _propertyInfo = propertyInfo;
//         _header = propertyInfo.Name;
//         PropertyName = propertyInfo.Name;
//     }

//     public ICsvColumnSetup<TProperty> SetHeader(string newHeader)
//     {
//         _header = newHeader;
//         return this;
//     }

//     public ICsvColumnSetup<TProperty> SetFormat(Expression<Func<TProperty, string>> expression)
//     {
//         _customFormat = new ValueFormatter<TProperty>(expression.Compile());
//         return this;
//     }

//     public string ReadValue<T>(T instance)
//     {
//         var value = _propertyInfo.GetValue(instance, null);

//         if (value is null) return string.Empty;

//         return _customFormat?.Format(value) ?? value.ToString() ?? string.Empty;
//     }

//     public string Header => _header;
// }

// class CsvBuilder<T>
// {
//     private readonly IEnumerable<T> _data;
//     private readonly IEnumerable<PropertyInfo> _properties;
//     private readonly Dictionary<string, ICsvColumn> _columns;

//     private string _delimiter = ",";

//     public CsvBuilder(IEnumerable<T> data)
//     {
//         _data = data;
//         _properties = PropertyInfoCache.GetCached(typeof(T));

//         _columns = _properties.Select(p => ((ICsvColumn)new CsvColumn(p)))
//         .ToDictionary(c => c.PropertyName);
//     }

//     public ICsvColumnSetup<TPropertyType> For<TPropertyType>(
//         Expression<Func<T, TPropertyType>> expression)
//     {

//         if (expression.Body is not MemberExpression memberExp)
//             throw new Exception("Invalid Expression");

//         var propName = memberExp.Member.Name;

//         return _columns[propName];
//     }

//     public CsvBuilder<T> SetDelimiter(string delimiter)
//     {
//         _delimiter = delimiter;
//         return this;
//     }

//     public byte[] ToBytes()
//     {
//         ThrowIfNotValid();

//         var sb = new StringBuilder();

//         sb.AppendJoin(_delimiter, _columns.Select(c => c.Value.Header));

//         foreach (var row in _data)
//         {
//             sb.AppendLine();
//             sb.AppendJoin(_delimiter, _columns.Select(c => c.Value.ReadValue(row)));
//         }

//         var csvContent = sb.ToString();

//         return Encoding.UTF8.GetBytes(csvContent);
//     }

//     private void ThrowIfNotValid()
//     {
//         ArgumentNullException.ThrowIfNull(_data, "data");
//     }
// }
// static class ObjectExtensions
// {
//     public static CsvBuilder<T> ToCsv<T>(this IEnumerable<T> data)
//     {
//         return new CsvBuilder<T>(data);
//     }
// }

// public interface IPropertyRule<TInstance>
// {
//     string GetColumnName();
//     string GetValue(TInstance instance);
// }

// class PropertyRule<TInstance, TProperty>
// {
//     private readonly PropertyInfo _propertyInfo;
//     public readonly Func<TProperty, string>? _format;
//     private readonly string _customColumnName;

//     public PropertyRule(PropertyInfo propertyInfo, string customColumnName, Func<TProperty, string>? format = null)
//     {
//         _propertyInfo = propertyInfo;
//         _format = format;
//         _customColumnName = customColumnName;
//     }

//     public string GetColumnName()
//     {
//         return string.IsNullOrWhiteSpace(_customColumnName) ? _propertyInfo.Name : _customColumnName;
//     }

//     public string GetValue(TInstance instance)
//     {
//         var value = _propertyInfo.GetValue(instance, null);

//         if (value is null) return string.Empty;

//         return (_format is null ? value.ToString() : _format((TProperty)value)) ?? string.Empty;
//     }
// }

// static class PropertyInfoCache
// {
//     private static readonly ConcurrentDictionary<Type, IEnumerable<PropertyInfo>> _cache = new();

//     public static IEnumerable<PropertyInfo> GetCached(Type type)
//     {
//         return _cache.GetOrAdd(type, valueFactory: (t) =>
//         {
//             return t.GetProperties().Where(p => p.CanRead);
//         });
//     }
// }