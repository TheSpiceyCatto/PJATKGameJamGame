using System;

namespace Managers
{
    public static class GameEventManager
    {
        public static event Action OnCutsceneEnd;
        
        public static void EndCutscene() {
            OnCutsceneEnd?.Invoke();
        }
    }
}
