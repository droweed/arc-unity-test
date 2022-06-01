using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace gotoandplay
{
    public class GameController : MonoBehaviour
    {
        public static GameController I;

        [HideInInspector]
        public UnityEvent<GameState> onGameStateChanged;

        private GameState gameState = GameState.IN_GAME;

        // current accumulated player points
        [SerializeField]
        private int currentPlayerPoints = 0;
        // out target points
        [SerializeField]
        private int goalPoints = GameConstants.GoalPoints;

        // gui hud reference
        private GamehudView gamehudView;

        // timer properties
        [SerializeField]
        private int currentTimer = 90;

        // out completion flag
        private bool isLevelComplete;
        public bool IsLevelComplete
        {
            get
            {
                return isLevelComplete;
            }
        }

        private void Awake()
        {
            if (I == null) { I = this; }
        }

        private void Start()
        {
            gamehudView = FindObjectOfType<GamehudView>();
            gamehudView.UpdatePointsLabel(currentPlayerPoints, goalPoints);
        }

        private void StartGame()
        {

        }

        public GameState GetGameState()
        {
            return gameState;
        }

        public void AddPoints(int value)
        {
            if (isLevelComplete)
                return;

            currentPlayerPoints += value;
            currentPlayerPoints = Mathf.Clamp(currentPlayerPoints, 0, goalPoints);
            gamehudView.UpdatePointsLabel(currentPlayerPoints, goalPoints);

            // main game condition
            // if points reached the goal points, game is complete
            if (currentPlayerPoints >= goalPoints)
            {
                SetGameState(GameState.COMPLETE);
            }
        }

        public void DeductPoints(int value)
        {
            currentPlayerPoints -= value;
            // clamp in case we get negative value

            currentPlayerPoints = Mathf.Clamp(currentPlayerPoints, 0, goalPoints);
            gamehudView.UpdatePointsLabel(currentPlayerPoints, goalPoints);

            // check if the player has 0 points after deduct, then its game over
            if (currentPlayerPoints <= 0)
            {
                // game over as well
                SetGameState(GameState.GAMEOVER);
            }
        }

        public void SetGameState(GameState state)
        {
            if (IsLevelComplete)
                return;

            gameState = state;
            onGameStateChanged.Invoke(gameState);

            switch (gameState)
            {
                case GameState.LOBBY:
                    break;
                case GameState.IN_GAME:
                    break;
                case GameState.COMPLETE:
                    isLevelComplete = true;
                    gamehudView.ShowLevelComplete();
                    break;
                case GameState.GAMEOVER:
                    isLevelComplete = true;
                    gamehudView.ShowGameOver();
                    break;
            }
        }

        /// <summary>
        /// Common method for scene reload/retry
        /// </summary>
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public enum GameState
    {
        LOBBY,
        IN_GAME,
        COMPLETE,
        GAMEOVER
    }
}