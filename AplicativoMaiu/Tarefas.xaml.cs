using System.Collections.ObjectModel;

namespace AplicativoMaiu;

public sealed class Tarefa
{
    public required string Nome { get; init; }

    public bool Concluida { get; set; }
}

public partial class Tarefas : ContentPage
{
    private readonly ObservableCollection<Tarefa> _tarefas = [];

    public Tarefas()
    {
        InitializeComponent();
        taskListView.ItemsSource = _tarefas;
    }

    private async void OnAddTaskClicked(object sender, EventArgs e)
    {
        var nome = taskEntry.Text?.Trim();

        if (string.IsNullOrEmpty(nome))
        {
            await DisplayAlert("Tarefa inválida", "Digite uma tarefa antes de adicionar.", "OK");
            return;
        }

        _tarefas.Add(new Tarefa
        {
            Nome = nome,
            Concluida = false
        });

        taskEntry.Text = string.Empty;
        taskEntry.Focus();
    }
}
