using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gotoandplay
{
    public class PlayerController : MonoBehaviour
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
    }
}