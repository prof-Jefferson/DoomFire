using DoomFire.App;
using DoomFire.Domain;
using DoomFire.Rendering;

// Program.cs: composição ("wire-up") do app.
// Aqui a gente monta as dependências e chama app.Run().

int width = Math.Min(Console.WindowWidth, 120) / 2; // /2 porque renderiza 2 espaços por "pixel"
int height = Math.Min(Console.WindowHeight, 45) - 1;

var palette = new DoomPalette();

var field = new FireField(
    width: Math.Max(20, width),
    height: Math.Max(20, height),
    maxIntensity: palette.Size - 1
);

IFireAlgorithm algorithm = new DoomFireAlgorithm(maxDecay: 3, windBias: 1);
IRenderer renderer = new AnsiConsoleRenderer(palette);

var app = new DoomFireApp(field, algorithm, renderer, fps: 60);
app.Run();
