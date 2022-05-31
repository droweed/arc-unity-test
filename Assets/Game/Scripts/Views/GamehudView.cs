using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace gotoandplay
{
    public class GamehudView : MonoBehaviour
    {
        public TextMeshProUGUI lblPointsCollected;

        public GameObject uiLevelComplete;
        public GameObject uiGameOver;

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
            uiLevelComplete.SetActive(true);
            uiGameOver.SetActive(false);
        }

        public void ShowGameOver()
        {
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