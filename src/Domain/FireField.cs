namespace DoomFire.Domain;

/// <summary>
/// FireField: estado do "mundo" do fogo.
/// É só uma matriz (largura x altura) de intensidades (0..MaxIntensity).
/// Não sabe nada de console, cores ou loop.
/// </summary>
public sealed class FireField
{
    public int Width { get; }
    public int Height { get; }
    public int MaxIntensity { get; }

    // Armazenamento linear (mais rápido e simples): index = y*Width + x.
    private readonly int[] _cells;

    public FireField(int width, int height, int maxIntensity)
    {
        if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
        if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));
        if (maxIntensity < 1) throw new ArgumentOutOfRangeException(nameof(maxIntensity));

        Width = width;
        Height = height;
        MaxIntensity = maxIntensity;

        _cells = new int[width * height];
    }

    public int Get(int x, int y) => _cells[y * Width + x];

    public void Set(int x, int y, int value)
    {
        // Checagem rápida e segura usando cast para uint
        if ((uint)x >= (uint)Width || (uint)y >= (uint)Height) return;
        _cells[y * Width + x] = Math.Clamp(value, 0, MaxIntensity);
    }

    /// <summary>
    /// "Acende" a base do fogo: a última linha recebe uma intensidade.
    /// No algoritmo clássico do DOOM, essa linha é a fonte de calor.
    /// </summary>
    public void SeedBottomRow(int intensity)
    {
        intensity = Math.Clamp(intensity, 0, MaxIntensity);
        int y = Height - 1;
        for (int x = 0; x < Width; x++)
            Set(x, y, intensity);
    }

    // Acesso interno para algoritmos mais rápidos (evita Get/Set em loop pesado).
    internal int[] Raw() => _cells;
}
