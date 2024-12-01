using System;

namespace Managers
{
    public static class GameEventManager
    {
        public static event Action OnCutsceneEnd;
        public static event Action OnEnemiesDefeated;
        
        public static void EndCutscene() {
            OnCutsceneEnd?.Invoke();
        }

        public static void EnemiesDefeated() {
            OnEnemiesDefeated?.Invoke();
        }
    }
}
