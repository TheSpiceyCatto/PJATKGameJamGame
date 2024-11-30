using System;

namespace Managers
{
    public static class PlayerEventManager
    {
        public static event Action<int> OnHealthUpdate;
        public static event Action OnDeath;

        public static void UpdateHealth(int currentHealth) {
            OnHealthUpdate?.Invoke(currentHealth);
        }

        public static void Death() {
            OnDeath?.Invoke();
        }
    }
}
