namespace MoMo.Modules.Firms.Domain.Model;

internal class ArFirm(Guid id, string name)
{
    internal Guid Id { get; } = id;
    internal string Name { get; } = name;
    
    private readonly List<Adviser> _advisers = [];
    internal IReadOnlyList<Adviser> Advisers => _advisers;

    internal void AddAdviser(Adviser adviser) => _advisers.Add(adviser);
}