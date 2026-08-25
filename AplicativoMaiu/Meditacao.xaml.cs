namespace AplicativoMaiu;

public partial class Meditacao : ContentPage
{
    private readonly IDispatcherTimer _temporizador;
    private TimeSpan _tempoRestante = TimeSpan.Zero;

    public Meditacao()
    {
        InitializeComponent();

        _temporizador = Dispatcher.CreateTimer();
        _temporizador.Interval = TimeSpan.FromSeconds(1);
        _temporizador.Tick += OnTemporizadorTick;
    }

    protected override void OnDisappearing()
    {
        _temporizador.Stop();
        AtualizarEstadoDosBotoes();
        base.OnDisappearing();
    }

    private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        if (pickerTempo.SelectedItem is not int minutos)
        {
            return;
        }

        _tempoRestante = TimeSpan.FromMinutes(minutos);
        AtualizarCronometro();
        AtualizarEstadoDosBotoes();
    }

    private void OnIniciarClicked(object sender, EventArgs e)
    {
        if (_tempoRestante <= TimeSpan.Zero || _temporizador.IsRunning)
        {
            return;
        }

        _temporizador.Start();
        AtualizarEstadoDosBotoes();
    }

    private void OnPararClicked(object sender, EventArgs e)
    {
        _temporizador.Stop();
        AtualizarEstadoDosBotoes();
    }

    private void OnReiniciarClicked(object sender, EventArgs e)
    {
        _temporizador.Stop();

        _tempoRestante = pickerTempo.SelectedItem is int minutos
            ? TimeSpan.FromMinutes(minutos)
            : TimeSpan.Zero;

        AtualizarCronometro();
        AtualizarEstadoDosBotoes();
    }

    private void OnTemporizadorTick(object? sender, EventArgs e)
    {
        if (_tempoRestante <= TimeSpan.Zero)
        {
            _temporizador.Stop();
            AtualizarEstadoDosBotoes();
            return;
        }

        _tempoRestante -= TimeSpan.FromSeconds(1);
        AtualizarCronometro();

        if (_tempoRestante == TimeSpan.Zero)
        {
            _temporizador.Stop();
            AtualizarEstadoDosBotoes();
        }
    }

    private void AtualizarCronometro()
    {
        lblCronometro.Text = _tempoRestante.ToString(@"mm\:ss");
    }

    private void AtualizarEstadoDosBotoes()
    {
        btnIniciar.IsEnabled = !_temporizador.IsRunning && _tempoRestante > TimeSpan.Zero;
        btnParar.IsEnabled = _temporizador.IsRunning;
        btnReiniciar.IsEnabled = pickerTempo.SelectedItem is int;
        pickerTempo.IsEnabled = !_temporizador.IsRunning;
    }
}
