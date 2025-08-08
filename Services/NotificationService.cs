using System;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaBlazor.Services
{
    public class NotificationService : IDisposable
    {
        public string? Message { get; private set; }
        public bool IsVisible { get; private set; }

        public event Action? OnChange;

        private CancellationTokenSource? _cancellationTokenSource;

        public void Show(string message)
        {
            // Inicia la tarea de mostrar y ocultar sin esperar a que termine ("fire and forget"),
            // para no bloquear la UI.
            _ = ShowAndHideAsync(message);
        }

        private async Task ShowAndHideAsync(string message)
        {
            Message = message;
            IsVisible = true;
            NotifyStateChanged();

            // Si ya hay una notificación mostrándose, cancela su temporizador de ocultación.
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                // Espera 3 segundos.
                await Task.Delay(3000, _cancellationTokenSource.Token);

                // Oculta la notificación después de la espera.
                IsVisible = false;
                NotifyStateChanged();
            }
            catch (TaskCanceledException)
            {
                // Esto ocurre si se muestra una nueva notificación antes de que la anterior se oculte.
                // Es un comportamiento esperado y no necesita acción.
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}