namespace DoomFire.Rendering;

/// <summary>
/// DoomPalette: paleta inspirada no "DOOM Fire".
/// Intensidade baixa = preto/escuro; alta = amarelo/branco.
/// </summary>
public sealed class DoomPalette : IPalette
{
    private static readonly Rgb[] Colors = new Rgb[]
    {
        new(  0,  0,  0),
        new(  7,  7,  7),
        new( 31,  7,  7),
        new( 47, 15,  7),
        new( 71, 15,  7),
        new( 87, 23,  7),
        new(103, 31,  7),
        new(119, 31,  7),
        new(143, 39,  7),
        new(159, 47,  7),
        new(175, 63,  7),
        new(191, 71,  7),
        new(199, 71,  7),
        new(223, 79,  7),
        new(223, 87,  7),
        new(223, 87,  7),
        new(215, 95,  7),
        new(215,103, 15),
        new(207,111, 15),
        new(207,119, 15),
        new(207,127, 15),
        new(207,135, 23),
        new(199,135, 23),
        new(199,143, 23),
        new(199,151, 31),
        new(191,159, 31),
        new(191,159, 31),
        new(191,167, 39),
        new(191,167, 39),
        new(191,175, 47),
        new(183,175, 47),
        new(183,183, 47),
        new(183,183, 55),
        new(207,207,111),
        new(223,223,159),
        new(239,239,199),
        new(255,255,255)
    };

    public int Size => Colors.Length;

    public Rgb this[int intensity]
    {
        get
        {
            if (intensity < 0) intensity = 0;
            if (intensity >= Colors.Length) intensity = Colors.Length - 1;
            return Colors[intensity];
        }
    }
}
