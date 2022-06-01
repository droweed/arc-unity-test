using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleInputNamespace;

namespace gotoandplay
{
    public class PlayerLocomotion : MonoBehaviour
    {
        public Joystick mJoystick;
        public bool useJoystickInput;

        public Transform playerModel;

        public float mSpeed = 12f;
        public float mTurnSpeed = 8f;
        
        private Rigidbody mRigidbody;
        private float mVerticalInputValue;
        private float mHorizontalInputValue;

        private Vector3 moveDirection;
        private Vector3 targetDir;

        private AnimationController animationController;

        private void Awake()
        {
            mRigidbody = GetComponent<Rigidbody>();
            animationController = GetComponent<AnimationController>();
        }


        private void OnEnable()
        {
            mVerticalInputValue = 0f;
            mHorizontalInputValue = 0f;
        }


        private void OnDisable()
        {
            mRigidbody.isKinematic = true;
        }


        private void Start()
        {

        }


        private void Update()
        {
            HandleUserInput();
        }

        private void FixedUpdate()
        {
            Move();
            Turn();
            HandleAnimation();
        }

        private void HandleUserInput()
        {
            if(useJoystickInput)
            {
                mVerticalInputValue = mJoystick.yAxis.value;
                mHorizontalInputValue = mJoystick.xAxis.value;
            }
            else
            {
                mVerticalInputValue = Input.GetAxis("Vertical");
                mHorizontalInputValue = Input.GetAxis("Horizontal");
            }
        }

        private void Move()
        {
            moveDirection = Camera.main.transform.forward * mVerticalInputValue;
            moveDirection += Camera.main.transform.right * mHorizontalInputValue;

            moveDirection.Normalize();
            moveDirection.y = 0;
            moveDirection *= mSpeed;

            mRigidbody.velocity = moveDirection;
        }

        private void Turn()
        {
            if (mRigidbody)
            {
                targetDir = Vector3.zero;

                targetDir = Camera.main.transform.forward * mVerticalInputValue;
                targetDir += Camera.main.transform.right * mHorizontalInputValue;

                if (targetDir != Vector3.zero)
                {
                    targetDir.Normalize();
                    targetDir.y = 0;

                    if (targetDir == Vector3.zero)
                        targetDir = transform.forward;

                    // rotation with interpolation
                    Quaternion tr = Quaternion.LookRotation(targetDir);
                    Quaternion targetRotation = Quaternion.Slerp(playerModel.rotation, tr, mTurnSpeed * Time.deltaTime);

                    playerModel.rotation = targetRotation;
                }
            }
        }

        private void HandleAnimation()
        {
            if(moveDirection != Vector3.zero)
            {
                animationController.SetState(AnimationController.AnimState.MOVE);
            } 
            else
            {
                animationController.SetState(AnimationController.AnimState.IDLE);
            }
        }

    }
}