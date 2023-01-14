using System.Text;
using App;

// var d = new Foo
// {
//     Value = 1,
//     Name = "Tos"
// };

// d.SaySomething("Hi, There")
// .SaySomething("This is not normal");

// Console.WriteLine("{0} has {1}", d.Name, d.Value);

var data = new Foo[] {
        new Foo{ Name = "Tos", Value = 1},
        new Foo{ Name = "Jiew", Value = 2}
    };

    //https://gist.github.com/MaciejLisCK/8804658

var contentBytes = data.CreateCsvBuilder()
    .Setup(e => e.Name, "Symbol Name")
    .Setup(e => e.Value, "FEE (Incl VAT)", value => value.ToString("D2"))
    .ToBytes();

    var csv = Encoding.UTF8.GetString(contentBytes);

Console.WriteLine(csv);
Console.WriteLine("----------");

// "Symbol Name,FEE (Incl VAT)\nTos,01\nJiew,02"
