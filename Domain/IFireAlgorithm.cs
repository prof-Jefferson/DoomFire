namespace DoomFire.Domain;

/// <summary>
/// Strategy: contrato de algoritmo de propagação do fogo.
/// A implementação decide como o campo evolui a cada frame.
/// </summary>
public interface IFireAlgorithm
{
    void Step(FireField field, Random rng);
}
