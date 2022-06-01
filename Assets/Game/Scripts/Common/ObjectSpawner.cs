using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lean.Pool;

namespace gotoandplay
{
    public class ObjectSpawner : MonoBehaviour
    {
        public float delay = 0;
        public GameObject spawnPrefab;

        private void Start()
        {
            DOVirtual.DelayedCall(delay, () =>
            {
                LeanPool.Spawn(spawnPrefab);
            });
        }
    }
}