namespace DoomFire.App;

using DoomFire.Domain;
using DoomFire.Rendering;

/// <summary>
/// DoomFireApp: "motorzinho" do programa.
/// Responsabilidades:
/// - Inicializar/encerrar o renderer
/// - Loop principal (FPS)
/// - Ler input (ESC para sair, SPACE para reacender)
/// - Chamar o algoritmo de simulação e renderizar
/// </summary>
public sealed class DoomFireApp
{
    private readonly FireField _field;
    private readonly IFireAlgorithm _algorithm;
    private readonly IRenderer _renderer;
    private readonly Random _rng;
    private readonly int _frameDelayMs;

    public DoomFireApp(FireField field, IFireAlgorithm algorithm, IRenderer renderer, int fps = 60, int? seed = null)
    {
        _field = field ?? throw new ArgumentNullException(nameof(field));
        _algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
        _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));

        if (fps < 1) throw new ArgumentOutOfRangeException(nameof(fps));
        _frameDelayMs = (int)Math.Round(1000.0 / fps);

        _rng = seed.HasValue ? new Random(seed.Value) : new Random();
    }

    public void Run()
    {
        _renderer.Initialize();
        _field.SeedBottomRow(_field.MaxIntensity);

        try
        {
            while (true)
            {
                HandleInput();
                _algorithm.Step(_field, _rng);
                _renderer.Render(_field);
                Thread.Sleep(_frameDelayMs);
            }
        }
        catch (OperationCanceledException)
        {
            // saída normal (ESC)
        }
        finally
        {
            _renderer.Shutdown();
        }
    }

    private void HandleInput()
    {
        if (!Console.KeyAvailable) return;

        var key = Console.ReadKey(intercept: true).Key;
        if (key == ConsoleKey.Escape)
            throw new OperationCanceledException();

        if (key == ConsoleKey.Spacebar)
            _field.SeedBottomRow(_field.MaxIntensity);
    }
}
