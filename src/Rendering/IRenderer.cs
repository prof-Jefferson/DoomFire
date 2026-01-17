namespace DoomFire.Rendering;

using DoomFire.Domain;

/// <summary>
/// Renderer: desenha um FireField em algum lugar.
/// Aqui é console ANSI, mas pode ser bitmap, janela, web, etc.
/// </summary>
public interface IRenderer
{
    void Initialize();
    void Render(FireField field);
    void Shutdown();
}
