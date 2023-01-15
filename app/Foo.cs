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