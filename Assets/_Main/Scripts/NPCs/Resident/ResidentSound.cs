using System.Collections;
using System.Collections.Generic;
using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.ScriptableObjects;
using _Main.Scripts.Utils;
using UnityEngine;

namespace _Main.Scripts.NPCs.Resident
{
    public class ResidentSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        
        [SerializeField] private ResidentConditionHintSettings _residentConditionHintSettings;
        [SerializeField] private ResidentOrderSettings _residentOrderSettings;
        [SerializeField] private float _delayBetweenClips = 0;

        private ResidentSoundsSettings _residentSoundsSettings;
        
        public void Init(ResidentSoundsSettings residentSoundsSettings) =>
            _residentSoundsSettings = residentSoundsSettings;
        
        public void Destruct(){}

        public void ShowConditionHint(ResidentConditionType residentCondition)
        {
            if (_residentConditionHintSettings.GetPair(residentCondition).AudioClips.Count> 0)
                _audioSource.PlayOneShot(_residentConditionHintSettings.GetPair(residentCondition).AudioClips.RandomElementFromList());
        }

        public void MakeOrder(Food food)
        {
            StartCoroutine(SpellOrder(PrepareSpell(food)));
        }

        private List<AudioClip> PrepareSpell(Food food)
        {
            List<AudioClip> audioClips = new List<AudioClip>();
            string spell = _residentOrderSettings.GetSpellString(food);

            foreach (var charSound in spell)
            {
                if (charSound == '*' || charSound == '.')
                    audioClips.Add(_residentSoundsSettings.ShortSounds.RandomElementFromList());
                if (charSound == '-' || charSound == '_')
                    audioClips.Add(_residentSoundsSettings.LongSounds.RandomElementFromList());
            }

            return audioClips;
        }

        private IEnumerator SpellOrder(List<AudioClip> audioClips)
        {
            foreach (AudioClip clip in audioClips)
            {
                if (clip == null)
                    continue;

                _audioSource.clip = clip;
                _audioSource.Play();

                while (_audioSource.isPlaying)
                    yield return null;

                if (_delayBetweenClips > 0f)
                    yield return new WaitForSeconds(_delayBetweenClips);
            }
        }
    }
}