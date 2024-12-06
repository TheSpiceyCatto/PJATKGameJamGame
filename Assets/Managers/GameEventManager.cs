using System;

namespace Managers
{
    public static class GameEventManager
    {
        public static event Action OnCutsceneEnd;
        public static event Action OnEnemiesDefeated;
        public static event Action OnDialogueEnd;
        public static event Action OnAscDialogueEnd;
        
        public static void EndCutscene() {
            OnCutsceneEnd?.Invoke();
        }

        public static void EnemiesDefeated() {
            OnEnemiesDefeated?.Invoke();
        }
        
        public static void DialogueEnded() {
            OnDialogueEnd?.Invoke();
        }

        public static void AscDialogueEnded()
        {
            OnAscDialogueEnd?.Invoke();
        }
    }
}
