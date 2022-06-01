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