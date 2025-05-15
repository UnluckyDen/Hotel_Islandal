using System.Collections.Generic;
using _Main.Scripts.Infrastructure.Tiles;
using _Main.Scripts.NPCs.Resident;
using _Main.Scripts.Player.Movement.Way;
using UnityEngine;

namespace _Main.Scripts.Infrastructure.Level
{
    public class LevelTiles : MonoBehaviour
    {
        [SerializeField] private ResidentsDistributor _residentsDistributor;
        [SerializeField] private List<LevelTile> _levelTiles;
 
        public ResidentsDistributor ResidentsDistributor => _residentsDistributor;

        [field:SerializeField] public List<WayPoint> WayPoints { get; private set; }
    }
}