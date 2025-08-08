using Domain;

namespace PruebaBlazor.Services
{
    public class UserStateService
    {
        public Usuario? CurrentUser { get; private set; }
        public int CartItemCount { get; private set; }

        public bool IsLoggedIn => CurrentUser != null;

        public event Action? OnChange;

        public void SetCurrentUser(Usuario? user)
        {
            CurrentUser = user;
            NotifyStateChanged();
        }

        public void SetCartItemCount(int count)
        {
            CartItemCount = count;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
