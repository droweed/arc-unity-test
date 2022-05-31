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

        public  float currentTimerValue = GameConstants.StartingTimerValue; // default value is 90
        
        private void Start()
        {
            // pretty sure this gets enabled only when start is pressed. (for now)
            Init();
        }

        private void Init()
        {
            // only run if we have reference to the text component
            if (label)
            {
                timerArray = currentTimerValue.ToString("0.00").Split(".");
                label.text = string.Format("{0}<size=32>.{1}s</size>", timerArray[0], timerArray[1]);
                DOVirtual.DelayedCall(2f, () =>
                {
                    Debug.Log("Game started!");
                    StartCoroutine(CoroutineTimerStart());
                });
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
    }
}