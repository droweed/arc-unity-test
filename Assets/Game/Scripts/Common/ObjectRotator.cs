using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gotoandplay
{
    public class ObjectRotator : MonoBehaviour
    {
        public float speed;

        private void Update()
        {
            transform.Rotate( new Vector3(0, speed * Time.deltaTime, 0), Space.Self);
        }
    }
}