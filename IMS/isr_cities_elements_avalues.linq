<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\mscorlib.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Linq.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xml.Linq.dll</Reference>
  <Namespace>System</Namespace>
  <Namespace>System.Linq</Namespace>
  <Namespace>System.Xml.Linq</Namespace>
</Query>

var doc = XDocument.Load(@"isr_cities.xml");
var names = from elementName in doc.Root.Descendants("ElementName")
		 	let elementValue = (XElement)elementName.NextNode
			group (string)elementValue by (string)elementName into values
			select new
			{
				Name = values.Key,
				Count = values.Count(),
				Values = string.Join("\n", values.Take(10)),
			};
					
foreach (var name in names)
{
	System.Console.WriteLine(name);
}