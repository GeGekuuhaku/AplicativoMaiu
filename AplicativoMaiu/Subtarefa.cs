using System.Collections.ObjectModel;

namespace AplicativoMaiu;

public sealed class TarefaItem
{
    public required string Titulo { get; init; }

    public bool Concluida { get; set; }

    public ObservableCollection<SubtarefaItem> Subtarefas { get; } = [];

    public bool TodasSubtarefasConcluidas =>
        Subtarefas.Count > 0 && Subtarefas.All(subtarefa => subtarefa.Concluida);
}

public sealed class SubtarefaItem
{
    public required string Titulo { get; init; }

    public bool Concluida { get; set; }
}
