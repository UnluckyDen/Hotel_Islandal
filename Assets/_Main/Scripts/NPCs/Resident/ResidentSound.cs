using System.Collections;
using System.Collections.Generic;
using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.ScriptableObjects;
using UnityEngine;

namespace _Main.Scripts.NPCs.Resident
{
    public class ResidentSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private AudioClip _short;
        [SerializeField] private AudioClip _long;
        [SerializeField] private ResidentConditionHintSettings _residentConditionHintSettings;
        [SerializeField] private ResidentOrderSettings _residentOrderSettings;
        [SerializeField] private float _delayBetweenClips = 0;

        public void ShowConditionHint(ResidentConditionType residentCondition)
        {
            if (_residentConditionHintSettings.GetPair(residentCondition).AudioClip != null)
                _audioSource.PlayOneShot(_residentConditionHintSettings.GetPair(residentCondition).AudioClip);
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
                    audioClips.Add(_short);
                if (charSound == '-' || charSound == '_')
                    audioClips.Add(_long);
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