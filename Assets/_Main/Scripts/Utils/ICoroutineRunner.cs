using System.Collections;
using UnityEngine;

namespace _Main.Scripts.Utils
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator routine);
        public void StopCoroutine(IEnumerator routine);
        public void StopCoroutine(Coroutine routine);
    }
}