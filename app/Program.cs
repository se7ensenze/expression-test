using System.Text;
using App;

var data = new Foo[] {
        new Foo{ Name = "Tos", Value = 1},
        new Foo{ Name = "Jiew", Value = 2}
    };

    //https://gist.github.com/MaciejLisCK/8804658

var contentBytes = data.CreateCsvBuilder()
    .Setup(e => e.Name, "Symbol Name, Property Name")
    .Setup(e => e.Value, "FEE (Incl VAT)", value => value.ToString("D2"))
    .ToBytes();

    var csv = Encoding.UTF8.GetString(contentBytes);

Console.WriteLine(csv);
Console.WriteLine("----------");

/*
"Symbol Name, Property Name","FEE (Incl VAT)"
"Tos","01"
"Jiew","02"
*/
