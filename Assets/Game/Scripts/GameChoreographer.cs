using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using SimpleInputNamespace;

namespace gotoandplay
{
    public class GameChoreographer : MonoBehaviour
    {
        public CinemachineVirtualCamera playerVC;
        public CinemachineVirtualCamera enemyVC;

        [Header("Joystick reference")]
        public GameObject mJoystick;

        // Start is called before the first frame update
        void Start()
        {
            LevelIntro();
        }

        private void LevelIntro()
        {
            // opening sequence
            // 1 - focus on enemy for 3 seconds
            DOVirtual.DelayedCall(3f, () =>
            {
                // 2 - focus on player
                playerVC.gameObject.SetActive(true);
                // delay for 3 seconds before actually starting the game.
                DOVirtual.DelayedCall(3f, () =>
                {
                    // start the game
                    mJoystick.SetActive(true);
                    GameController.I.SetGameState(GameState.IN_GAME);
                });
            });
        }
    }
}