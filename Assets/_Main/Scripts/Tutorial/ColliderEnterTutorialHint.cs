using UnityEngine;

namespace _Main.Scripts.Tutorial
{
    public class ColliderEnterTutorialHint : BaseTutorialHint
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player.Player>())
                ShowHint();
        }
    }
}
