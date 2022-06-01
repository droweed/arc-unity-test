using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace gotoandplay
{
    /// <summary>
    /// enemy brain class which oversees the collective 
    /// behavior of the enemy
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        public Transform target;
        public float damageInterval = 0.5f;
        private bool canDamage;

        private void Start()
        {
            Init();
        }

        void Init()
        {
            canDamage = true;
        }

        private void OnCollisionStay(Collision other)
        {
            if (other.gameObject.CompareTag("Player") && canDamage)
            {
                var collidable = other.gameObject.GetComponent<ICollidable>();
                collidable.DeductPoints(GameConstants.HitDeductValue);

                canDamage = false;
                DOVirtual.DelayedCall(damageInterval, () =>
                {
                    canDamage = true;
                });
            }
        }
    }
}