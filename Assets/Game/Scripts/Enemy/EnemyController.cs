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

        public AudioClip collideClip;

        private bool canDamage;

        private void Start()
        {
            Init();
        }

        void Init()
        {
            canDamage = true;
            if(target == null)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        private void OnCollisionStay(Collision other)
        {
            if (other.gameObject.CompareTag("Player") && CanInteract())
            {
                var collidable = other.gameObject.GetComponent<ICollidable>();
                // deduct player points
                collidable.DeductPoints(GameConstants.HitDeductValue);
                // play collide sfx
                AudioController.Instance.PlayOneShot(collideClip);

                // we toggle this flag so enemy cannot spam damage.
                canDamage = false;
                DOVirtual.DelayedCall(damageInterval, () =>
                {
                    canDamage = true;
                });
            }
        }

        private bool CanInteract()
        {
            return canDamage && !GameController.I.IsLevelComplete;
        }
    }
}