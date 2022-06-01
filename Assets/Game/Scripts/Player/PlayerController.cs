using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gotoandplay
{
    public class PlayerController : MonoBehaviour, ICollidable
    {
        private void Start()
        {

        }

        public void OnTriggerEnter(Collider other)
        {
            var pickupable = other.GetComponent<IPickupable>();
            if(pickupable != null)
            {
                pickupable.Pickup();
            }
        }

        #region - interface methods
        public void DeductPoints(int value)
        {
            GameController.I.DeductPoints(value);
        }
        #endregion
    }
}