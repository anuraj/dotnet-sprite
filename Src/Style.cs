internal class Style
{
    public required string Name { private get; set; }
    public int Width { private get; set; }
    public int Height { private get; set; }
    public int Left { private get; set; }
    public bool UseClass { private get; set; }

    public override string ToString()
    {
        return string.Format("{4}{0} {{ width: {1}px; height: {2}px; background: url(SpriteImage.png) -{3}px 0;}}",
            Path.GetFileNameWithoutExtension(Name).ToLowerInvariant(), Width, Height, Left, UseClass ? "." : "#");
    }
}