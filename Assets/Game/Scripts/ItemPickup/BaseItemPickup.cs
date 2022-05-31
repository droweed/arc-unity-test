using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gotoandplay
{
    public class BaseItemPickup : MonoBehaviour, IPickupable
    {
        public int points = 1;

        public void Pickup()
        {
            GameController.I.AddPoints(points);
            Destroy(gameObject);
        }
    }
}