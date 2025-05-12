using _Main.Scripts.Services;

namespace _Main.Scripts.Tutorial
{
    public class OpenJournalTutorialHint : ColliderEnterTutorialHint
    {
        private void Start()
        {
            InputService.Instance.OpenBook += InputServiceOnOpenBook;
        }

        private void OnDestroy()
        {
            InputService.Instance.OpenBook -= InputServiceOnOpenBook;
        }

        private void InputServiceOnOpenBook()
        {
            HideHint();
        }
    }
}