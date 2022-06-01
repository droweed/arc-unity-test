using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace gotoandplay
{
    public class BaseItemPickup : MonoBehaviour, IPickupable
    {
        public int points = 1;
        public AudioClip pickupClip;
        public GameObject model;
        float respawnTimeAfterPickup = 15f;
        private Collider mCollider;

        private void Start()
        {
            mCollider = GetComponent<Collider>();
            Init();
        }

        public void Pickup()
        {
            GameController.I.AddPoints(points);
            AudioController.Instance.PlayOneShot(pickupClip);
            
            Deactivate();
        }

        private void Deactivate()
        {
            mCollider.enabled = false;
            model.SetActive(false);

            DOVirtual.DelayedCall(respawnTimeAfterPickup, () =>
            {
                // reactivate pickup item
                Init();
            });
        }

        private void Init()
        {
            // activate/re-activate collider
            mCollider.enabled = true;
            model.SetActive(true);
        }


    }
}