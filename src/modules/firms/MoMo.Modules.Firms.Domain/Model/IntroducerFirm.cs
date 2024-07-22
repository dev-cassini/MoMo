namespace MoMo.Modules.Firms.Domain.Model;

internal class IntroducerFirm(Guid id, string name)
{
    internal Guid Id { get; } = id;
    internal string Name { get; } = name;
    
    private readonly List<ArFirm> _authorisedArFirms = [];
    internal IReadOnlyList<ArFirm> AuthorisedArFirms => _authorisedArFirms;

    internal void AddAuthorisedArFirm(ArFirm authorisedArFirm) => _authorisedArFirms.Add(authorisedArFirm);
}