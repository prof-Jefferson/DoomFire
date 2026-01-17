namespace DoomFire.Rendering;

/// <summary>
/// Paleta: mapeia intensidade (0..N) para uma cor.
/// Assim o "mundo" usa números, e o renderer escolhe como pintar.
/// </summary>
public interface IPalette
{
    int Size { get; }
    Rgb this[int intensity] { get; }
}
