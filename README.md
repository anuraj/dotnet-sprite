# dotnet-sprite

A utility designed to produce a CSS sprite from a folder of images. An image sprite consolidates multiple images into a single file. Websites containing numerous images may experience prolonged loading times and trigger numerous server requests. Utilizing image sprites minimizes server requests, conserving bandwidth. This tool facilitates the creation of a sprite image along with its corresponding CSS file.

Learn more about [Implementing image sprites in CSS](https://developer.mozilla.org/en-US/docs/Web/CSS/CSS_Images/Implementing_image_sprites_in_CSS)

[![Build Status][ci-badge]][ci] ![NuGet][nuget-badge] ![NuGet Downloads][nuget-download-badge]

[ci]: https://github.com/anuraj/dotnet-sprite/actions/workflows/main.yml/badge.svg
[ci-badge]: https://github.com/anuraj/dotnet-sprite/actions/workflows/main.yml/badge.svg
[nuget]: https://www.nuget.org/packages/dotnet-sprite/
[nuget-badge]: https://img.shields.io/nuget/v/dotnet-sprite.svg?style=flat-square
[nuget-download-badge]: https://img.shields.io/nuget/dt/dotnet-sprite?style=flat-square

### Get started

Install .NET 6 or newer and run this command:

`dotnet tool install --global dotnet-sprite`

Run the tool with `dotnet-sprite` command.

### Usage
```
Description:
  A tool to generate a CSS sprite from a directory of images

Usage:
  dotnet-sprite <source> [options]

Arguments:
  <source>  Source images directory [default: C:\Windows]

Options:
  -o, --output <output>  Output directory
  -c, --use-classes      Use classes instead of ids in the CSS file [default: False]
  --version              Show version information
  -?, -h, --help         Show help and usage information

```