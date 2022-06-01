using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace gotoandplay
{
    public class GamehudView : MonoBehaviour
    {
        public TextMeshProUGUI lblPointsCollected;

        public GameObject uiLevelComplete;
        public GameObject uiGameOver;

        [Header("SFX Reference")]
        public AudioClip fanfareClip;
        public AudioClip gameoverClip;

        private void Start()
        {
        }

        public void UpdatePointsLabel(int value, int goal)
        {
            lblPointsCollected.text = string.Format("{0}/{1}", value, goal);;
        }

        #region - game state ui toggle methods
        public void ShowLevelComplete()
        {
            DOVirtual.DelayedCall(0.3f, () =>
            {
                AudioController.Instance.PlayOneShot(fanfareClip);
            });

            uiLevelComplete.SetActive(true);
            uiGameOver.SetActive(false);
        }

        public void ShowGameOver()
        {
            DOVirtual.DelayedCall(0.3f, () =>
            {
                AudioController.Instance.PlayOneShot(gameoverClip);
            });

            uiLevelComplete.SetActive(false);
            uiGameOver.SetActive(true);
        }
        #endregion

        #region scene navigation
        public void RestartLevel()
        {
            GameController.I.RestartLevel();
        }
        #endregion
    }
}