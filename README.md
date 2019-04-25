# Cross Platform Kindlegen .NET Wrapper

Create  Mobi file for Amazon Kindle. This library is able to create Mobi file easilly in .NET & .NET Core Applications.

## Getting Started

```bash
dotnet add package Kindlegen
```

or

```powershell
Install-Package Kindlegen
```

### Sample Code

```csharp
      Console.Write("Write your .opf .htm .html .epub .zip or directory path: ");
      string path = Console.ReadLine();
      var result = KindleConverter.Create(path)
          .SetCompressionLevel(CompressionLevel.NoCompression)
          .SetOutput("test.mobi")
          .Convert();

      if (!result.IsSuccess)
      {
          string message = result.Details.FirstOrDefault(x => x.ConvertLogType == ConvertLogType.Error)?.Message;
          Console.WriteLine($"Has exception: {message}");
      }
      else
      {
          Console.WriteLine("Complete successfully");
      }
```

## TODO

- [ ] Unit Testing
- [ ] More Examples

## LICENSE

This project is licensed under the MIT Lıcense
