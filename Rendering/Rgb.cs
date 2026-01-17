namespace DoomFire.Rendering;

/// <summary>
/// Tipo simples de cor em RGB (0..255).
/// Record struct: valor imutável, leve e prático.
/// </summary>
public readonly record struct Rgb(byte R, byte G, byte B);
