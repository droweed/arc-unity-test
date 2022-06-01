using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace gotoandplay
{
    public class EnemyLocomotion : MonoBehaviour
    {
        EnemyController controller;
        AIPath aiController;
        AIDestinationSetter destinationSetter;
        AnimationController animationController;

        private void Start()
        {
            Init();
            SubscribeEvents();
        }

        private void OnDestroy()
        {
            UnSubscribeEvents();
        }

        private void Init()
        {
            controller = GetComponent<EnemyController>();
            destinationSetter = GetComponent<AIDestinationSetter>();
            aiController = GetComponent<AIPath>();
            animationController = GetComponent<AnimationController>();
        }

        private void FixedUpdate()
        {
            if(aiController && animationController)
            {
                if(aiController.movementVelocity != Vector2.zero)
                {
                    animationController.SetState(AnimationController.AnimState.MOVE);
                } 
                else
                {
                    animationController.SetState(AnimationController.AnimState.IDLE);
                }
            }
        }

        #region - event sub methods
        private void SubscribeEvents()
        {
            if (GameController.I)
            {
                GameController.I.onGameStateChanged.AddListener(GameStateChangeHandler);
            }
        }
        private void UnSubscribeEvents()
        {
            if (GameController.I)
            {
                GameController.I.onGameStateChanged.RemoveListener(GameStateChangeHandler);
            }
        }

        private void GameStateChangeHandler(GameState newState)
        {
            switch (newState)
            {
                case GameState.IN_GAME:
                    // game has started, tell ai to chase player.
                    destinationSetter.target = controller.target;
                    break;
                case GameState.COMPLETE:
                case GameState.GAMEOVER:
                    // lets stop ai from chasing once the game is over/complete.
                    destinationSetter.target = null;
                    break;
            }
        }
        #endregion
    }
}