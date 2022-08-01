<p align="center">
  <img src="https://github.com/Holoon/NewtonsoftUtils/raw/main/doc/logo.png" width="256" title="Newtonsoft Logo">
</p>

# Holoon.NewtonsoftUtils

Utility packages for Newtonsoft.Json.

## Holoon.NewtonsoftUtils.CanBeUndefined

Allows to serialize or deserialize only a part of an object. 

### Installation 

```
Install-Package Holoon.NewtonsoftUtils.CanBeUndefined
```

Nuget package: https://www.nuget.org/packages/Holoon.NewtonsoftUtils.CanBeUndefined/

### Usage

#### Serialization

```c#
public class MyClass
{
    public string Property1 { get; set; }
    public CanBeUndefined<int> Property2 { get; set; }
    public CanBeUndefined<MyClass2> Property3 { get; set; }
    public CanBeUndefined<int> Property4 { get; set; }
    public int? Property5 { get; set; }
}

var myObject = new MyClass
{
    Property1 = "Arthur Dent",
    Property2 = Undefined.Value,
    Property3 = null,
    Property4 = 42,
    Property5 = null
}
var settings = new Newtonsoft.Json.JsonSerializerSettings
{
    ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
};
var json = Newtonsoft.Json.JsonConvert.SerializeObject(myObject, settings);
```

Result:

```json
{
    "Property1": "Arthur Dent",
    "Property3": null,
    "Property4": 42,
    "Property5": null
}
```

#### Deserialization

```c#
public class MyClass
{
    public string Property1 { get; set; }
    public CanBeUndefined<int> Property2 { get; set; }
    public CanBeUndefined<MyClass2> Property3 { get; set; }
    public CanBeUndefined<int> Property4 { get; set; }
    public int? Property5 { get; set; }
}

var jSon = @"{
    ""Property1"": ""Arthur Dent"",
    ""Property3"": null,
    ""Property4"": 42,
    ""Property5"": null
}";

var settings = new Newtonsoft.Json.JsonSerializerSettings
{
    ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
};
var result = Newtonsoft.Json.JsonConvert.DeserializeObject<MyClass>(jSon, settings);

// result.Property1 = "Arthur Dent"
// result.Property2.IsUndefined = true
// result.Property2.Value = 0
// result.Property3.IsUndefined = false
// result.Property3.Value = null
// result.Property4.IsUndefined = false
// result.Property4.Value = 42
// result.Property5 = null
```

## Holoon.NewtonsoftUtils.Trimming

Allows to trim all strings by default when serializing and/or deserializing an object.  
To ignore the trim for some properties to serialize/deserialize :

- use the `SpacedString` type,  
- use the `SpacedStringAttribute` attribute,  
- or use the `StringPropertiesToNotTrim` property
	
(see example bellow)

### Installation 

```
Install-Package Holoon.NewtonsoftUtils.Trimming
```

Nuget package: https://www.nuget.org/packages/Holoon.NewtonsoftUtils.Trimming/

### Usage

```c#
public class MyClass
{
    public string Property1 { get; set; }
    public SpacedString Property2 { get; set; }
    [Holoon.NewtonsoftUtils.Trimming.SpacedString]
    public string Property3 { get; set; }
    public string Property4 { get; set; }
}

var myObject = new MyClass
{
    Property1 = "   Arthur Dent   ",
    Property2 = "   Arthur Dent   ",
    Property3 = "   Arthur Dent   ",
    Property4 = "   Arthur Dent   "
}

var settings = new Newtonsoft.Json.JsonSerializerSettings();
var converter = new TrimmingConverter
	readJsonTrimmingOption: TrimmingOption.TrimEnd,
	writeJsonTrimmingOption: TrimmingOption.TrimBoth);
converter.StringPropertiesToNotTrim.Add<MyClass>(o => o.Property4);
settings.Converters.Add(converter);

// Serialization
var json = Newtonsoft.Json.JsonConvert.SerializeObject(myObject, settings);

// JSON: { "Property1": "Arthur Dent", "Property2": "   Arthur Dent   ", "Property3": "   Arthur Dent   ", "Property4": "   Arthur Dent   " }

// Deserialization
var result = Newtonsoft.Json.JsonConvert.DeserializeObject<MyClass>(json, settings);

// result.Property1 = "   Arthur Dent"
// result.Property2 = "   Arthur Dent   "
// result.Property3 = "   Arthur Dent   "
// result.Property4 = "   Arthur Dent   "

```

*NOTE:* 

- To ignore some properties to trim : 
	- using `SpacedString` as a type instead of string, 
	- using `SpacedStringAttribute` on a property of type `string`, 
	- or adding a `string` properties to the `StringPropertiesToNotTrim` collection is equivalent.
- `SpacedStringAttribute` on a property of an other type as `string` is ignored. 
- Non `string` properties added on the `StringPropertiesToNotTrim` collection are ignored. 

## Quick Links

Json.NET web site: https://www.newtonsoft.com/json  
Newtonsoft.Json repository: https://github.com/JamesNK/Newtonsoft.Json  

## TODO and known limitations

- `List<CanBeUndefined<int>>` are not properly handled, for now, please use `CanBeUndefined<List<int>>` instead.

## Contributing

If you'd like to contribute, please fork the repository and use a feature branch. Pull requests are welcome. Please respect existing style in code.

## Licensing

The code in this project is licensed under BSD-3-Clause license.
