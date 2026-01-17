namespace DoomFire.Rendering;

using System.Text;
using DoomFire.Domain;

/// <summary>
/// AnsiConsoleRenderer: renderiza o fogo no terminal usando ANSI (VT100).
/// - Usa cor de fundo (48;2;R;G;B) e imprime "  " como se fosse um pixel.
/// - Funciona muito bem no Linux e em terminais modernos.
/// </summary>
public sealed class AnsiConsoleRenderer : IRenderer
{
    private readonly IPalette _palette;
    private readonly StringBuilder _sb;

    public AnsiConsoleRenderer(IPalette palette)
    {
        _palette = palette ?? throw new ArgumentNullException(nameof(palette));
        _sb = new StringBuilder(1024);
    }

    public void Initialize()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.CursorVisible = false;
        // limpa tela, move cursor para home e esconde cursor
        Console.Write("\x1b[2J\x1b[H\x1b[?25l");
    }

    public void Render(FireField field)
    {
        _sb.Clear();
        _sb.Append("\x1b[H"); // cursor home

        for (int y = 0; y < field.Height; y++)
        {
            for (int x = 0; x < field.Width; x++)
            {
                int intensity = field.Get(x, y);
                var c = _palette[intensity];

                // Fundo colorido + dois espaços (pixel "quadrado")
                _sb.Append($"\x1b[48;2;{c.R};{c.G};{c.B}m  ");
            }
            _sb.Append("\x1b[0m\n"); // reset no fim da linha
        }

        Console.Write(_sb.ToString());
    }

    public void Shutdown()
    {
        Console.Write("\x1b[0m\x1b[?25h");
        Console.CursorVisible = true;
    }
}
