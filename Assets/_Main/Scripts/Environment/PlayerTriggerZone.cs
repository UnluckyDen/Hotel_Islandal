using System;
using UnityEngine;

namespace _Main.Scripts.Environment
{
    public class PlayerTriggerZone : MonoBehaviour
    {
        public event Action<Player.Player, bool> PlayerEnterTriggerZone; 
        public bool PlayerInZone { get; private set; }
        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<Player.Player>();
            if (player != null)
                PlayerEnter(player);
        }

        private void OnTriggerExit(Collider other)
        {
            var player = other.GetComponent<Player.Player>();
            if (player != null)
                PlayerExit(player);
        }

        private void PlayerEnter(Player.Player player)
        {
            PlayerInZone = true;
            PlayerEnterTriggerZone?.Invoke(player, true);
        }

        private void PlayerExit(Player.Player player)
        {
            PlayerInZone = false;
            PlayerEnterTriggerZone?.Invoke(player, false);
        }
    }
}