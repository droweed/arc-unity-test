using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace gotoandplay
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField]
        private float timerUpdateFreq = 0.025f;

        public TextMeshProUGUI label;

        public  float currentTimerValue; // default value is 90
        
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
            currentTimerValue = GameConstants.StartingTimerValue;

            // only run if we have reference to the text component
            if (label)
            {
                timerArray = currentTimerValue.ToString("0.00").Split(".");
                label.text = string.Format("{0}<size=32>.{1}s</size>", timerArray[0], timerArray[1]);
            }
        }

        string[] timerArray;
        private IEnumerator CoroutineTimerStart()
        {
            // only run when game state is in_game
            while ((GameController.I != null 
                && GameController.I.GetGameState() == GameState.IN_GAME 
                && !GameController.I.IsLevelComplete) && currentTimerValue > 0)
            {
                yield return new WaitForSeconds(timerUpdateFreq);
                currentTimerValue -= timerUpdateFreq;
                timerArray = currentTimerValue.ToString("0.00").Split(".");
                if (timerArray != null && timerArray.Length > 0)
                    label.text = string.Format("{0}<size=32>.{1}s</size>", timerArray[0], timerArray[1]);
                else
                {
                    // fallback
                    label.text = currentTimerValue + "";
                }
            }

            // reset to 0 once the timer is complete.
            currentTimerValue = Mathf.Clamp(currentTimerValue, 0, GameConstants.StartingTimerValue);
            timerArray = currentTimerValue.ToString("0.00").Split(".");
            if (timerArray != null && timerArray.Length > 0)
                label.text = string.Format("{0}<size=32>.{1}s</size>", timerArray[0], timerArray[1]);
            else
            { label.text = currentTimerValue + ""; }

            // set game state to gameover since time ran out.
            GameController.I.SetGameState(GameState.GAMEOVER);
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
                    // game started, start timer.
                    StartCoroutine(CoroutineTimerStart());
                    break;
                case GameState.COMPLETE:
                case GameState.GAMEOVER:
                    break;
            }
        }
        #endregion
    }
}