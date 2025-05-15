using _Main.Scripts.Player.Movement.Way;
using UnityEngine;

namespace _Main.Scripts.Infrastructure.Tiles
{
    public class LevelTile : MonoBehaviour
    {
        [SerializeField] private WayPoint _wayPoint;

        public WayPoint WayPoint => _wayPoint;
    }
}