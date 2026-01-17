namespace DoomFire.Domain;

/// <summary>
/// DoomFireAlgorithm: algoritmo clássico popularizado pelo DOOM.
/// Ideia:
/// - Cada célula "puxa" energia da célula de baixo.
/// - Aplica um decaimento aleatório (perde energia ao subir).
/// - Desloca um pouco para o lado (dá o efeito de "vento" e turbulência).
/// </summary>
public sealed class DoomFireAlgorithm : IFireAlgorithm
{
    private readonly int _maxDecay; // ex.: 3 -> decay 0..2
    private readonly int _windBias; // ex.: 1 -> deslocamento lateral base

    public DoomFireAlgorithm(int maxDecay = 3, int windBias = 1)
    {
        if (maxDecay < 1) throw new ArgumentOutOfRangeException(nameof(maxDecay));
        _maxDecay = maxDecay;
        _windBias = windBias;
    }

    public void Step(FireField field, Random rng)
    {
        int w = field.Width;
        int h = field.Height;
        var cells = field.Raw();

        // Não processa a última linha: ela é a "fonte".
        for (int y = 0; y < h - 1; y++)
        {
            int rowStart = y * w;
            for (int x = 0; x < w; x++)
            {
                int i = rowStart + x;
                int below = i + w;

                int decay = rng.Next(0, _maxDecay);
                int intensity = cells[below] - decay;
                if (intensity < 0) intensity = 0;

                // deslocamento lateral (vento/turbulência)
                int dstX = x - decay + _windBias;
                if (dstX < 0) dstX = 0;
                if (dstX >= w) dstX = w - 1;

                cells[rowStart + dstX] = intensity;
            }
        }
    }
}
