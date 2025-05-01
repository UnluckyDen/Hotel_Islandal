using System;
using _Main.Scripts.NPCs.Resident;
using UnityEngine;

namespace _Main.Scripts.Infrastructure
{
    public class LevelBootstrap : MonoBehaviour
    {
        [SerializeField] private ResidentsDistributor _residentsDistributor;

        private void Awake()
        {
            Init();
        }

        private void OnDestroy()
        {
            Destruct();
        }

        private void Init()
        {
            _residentsDistributor.Init();
        }

        private void Destruct()
        {
            _residentsDistributor.Destruct();
        }
    }
}