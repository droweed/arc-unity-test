using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gotoandplay
{
    public class AnimationController : MonoBehaviour
    {
        Animator mAnimator;

        public enum AnimState
        {
            IDLE,
            MOVE
        }

        private AnimState animState = AnimState.IDLE;

        private void Start()
        {
            mAnimator = GetComponentInChildren<Animator>();
        }

        public void SetState(AnimState newState)
        {
            // should only be triggered based on state change
            if(animState != newState)
            {
                animState = newState;
                SetAnimationByState();
            }
        }

        private void SetAnimationByState()
        {
            switch (animState)
            {
                case AnimState.IDLE:
                    mAnimator.CrossFade("Idle", 0.2f);
                    break;
                case AnimState.MOVE:
                    mAnimator.CrossFade("Run", 0.2f);
                    break;
            }
        }
    }
}