using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gotoandplay
{
    public interface IPickupable
    {
        void Pickup();
    }

    public interface ICollidable
    {
        void DeductPoints(int value);
    }

    public interface IKillable
    {
        void TakeDamage();
    }
}