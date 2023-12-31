using System.CommandLine;
using Microsoft.Extensions.Logging;
using SkiaSharp;

var outputOption = new Option<string>(
    aliases: ["-o", "--output"],
    description: "Output directory")
{
    IsRequired = false
};

var useClssesOption = new Option<bool>(
    aliases: ["-c", "--use-classes"],
    description: "Use classes instead of ids in the CSS file")
{
    IsRequired = false
};

useClssesOption.SetDefaultValue(false);

var rootCommand = new RootCommand("A tool to generate a CSS sprite from a directory of images")
{
    outputOption, useClssesOption
};
rootCommand.Name = "dotnet-sprite";

var sourceArgument = new Argument<string>("source")
{
    Arity = ArgumentArity.ExactlyOne,
    Description = "Source images directory"
};

sourceArgument.SetDefaultValue(Directory.GetCurrentDirectory());
rootCommand.Add(sourceArgument);

rootCommand.SetHandler((source, output, useClasses) =>
{
    try
    {
        if (!Directory.Exists(source))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Source directory {source} does not exist.");
            Console.ResetColor();
        }

        if (string.IsNullOrEmpty(output))
        {
            output = source;
        }
        else
        {
            if (!Directory.Exists(output))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Output directory does not exist. Creating it now.");
                Console.ResetColor();
            }
        }

        var files = Directory.GetFiles(source, "*.*", SearchOption.AllDirectories)
            .Where(file => file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".jpeg"));

        if (!files.Any())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"No images found in the source directory {source}");
            Console.ResetColor();
        }
        else
        {
            var styles = new List<Style>();
            var images = new List<SKBitmap>();
            int width = 0;
            int height = 0;

            foreach (string image in files)
            {
                var bitmap = SKBitmap.Decode(image);
                width += bitmap.Width;
                height = bitmap.Height > height ? bitmap.Height : height;
            }

            var finalImage = new SKBitmap(width, height);
            using var canvas = new SKCanvas(finalImage);
            canvas.Clear(SKColors.Transparent);
            int offset = 0;
            foreach (var file in files)
            {
                var image = SKBitmap.Decode(file);
                canvas.DrawBitmap(image, offset, 0);
                offset += image.Width;

                styles.Add(new Style
                {
                    Height = image.Height,
                    Width = image.Width,
                    Name = Path.GetFileName(file),
                    Left = offset - image.Width,
                    UseClass = useClasses
                });
            }

            using var stream = new FileStream(Path.Combine(output!, "SpriteImage.png"), FileMode.Create);
            finalImage.Encode(SKEncodedImageFormat.Png, 100).SaveTo(stream);
            var cssFile = Path.Combine(output, "style.css");
            using var streamWriter = new StreamWriter(cssFile, false);
            foreach (var style in styles)
            {
                streamWriter.Write(style.ToString());
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Sprite image and CSS file generated successfully at {output}");
            Console.ResetColor();
        }
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(ex.Message);
        Console.ResetColor();
    }
}, sourceArgument, outputOption, useClssesOption);

rootCommand.Name = "dotnet-sprite";

return rootCommand.Invoke(args);