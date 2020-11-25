using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayManager : MonoBehaviour {

    public UnityEvent onGameLost;
    public UnityEvent onGameReset;
    public UnityEvent onLevelCompleted;
    public UnityEvent onGamePause;
    public UnityEvent onGameResume;
    public UnityAction<int> onLifeChange;
    public UnityAction<int> onScoreChange;
    public UnityAction<int> onHighestScoreSet;
    public int health = 3;
    public int score = 0;
    public int levelsCompleted = 0;
    public int highestScore;
    public bool isPaused = false;
    public bool isStarted = false;

    [SerializeField] MapGenerator mapGenerator;
    [SerializeField] SaveDataGenerator dataGenerator;

    bool isGameRunning;

    #region SINGLETON PATTERN
    public static GameplayManager _instance;
    public static GameplayManager Instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<GameplayManager>();

                if (_instance == null) {
                    GameObject container = new GameObject("GameplayManager");
                    _instance = container.AddComponent<GameplayManager>();
                }
            }

            return _instance;
        }
    }
    #endregion


    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseLogic();
        }
    }

    public void Continue() {
        if (isPaused) {
            PauseLogic();
        }
    }

    public void LoadGame() {
        dataGenerator.LoadTheProgress();
        isStarted = true;
        highestScore = dataGenerator.LoadHighestScore();
    }

    public void ProceedToNextLevel() {
        levelsCompleted++;
        mapGenerator.LoadOrGenerateMap(levelsCompleted);
        if (onLevelCompleted != null) {
            onLevelCompleted.Invoke();
        }
    }

    public void StartGame() {
        RestartSession();
        isStarted = true;
        highestScore = dataGenerator.LoadHighestScore();
    }

    public void ExitGame() {
        dataGenerator.SaveTheHighestScore();
        dataGenerator.SaveTheProgress();
        Application.Quit();
    }

    public void RestartSession() {
        SetHealthValue(3);
        SetScoreValue(0);
        levelsCompleted = 0;
        mapGenerator.LoadOrGenerateMap(levelsCompleted);
        dataGenerator.SaveTheHighestScore();
        UnPause();
        if (onGameReset != null) {
            onGameReset.Invoke();
        }
    }

    public void LostBall() {
        IncreaseHealthValue(-1);
    }

    public void SetHealthValue(int value) {
        health = value;
        if (onLifeChange != null) {
            onLifeChange.Invoke(health);
        }
    }

    public void SetScoreValue(int value) {
        score = value;
        if (onScoreChange != null) {
            onScoreChange.Invoke(score);
        }
        CheckHeighestScore();
    }

    public void IncreaseHealthValue(int value) {
        health += value;
        if (onLifeChange != null) {
            onLifeChange.Invoke(health);
        }
        if (health <= 0) {

            GameLost();
        }
    }

    public void IncreaseScore(int value) {
        score += value;
        if (onScoreChange != null) {
            onScoreChange.Invoke(score);
        }
        CheckHeighestScore();
    }

    void CheckHeighestScore() {
        if (score > highestScore) {
            highestScore = score;
            if (onHighestScoreSet != null) {
                onHighestScoreSet.Invoke(highestScore);
            }

        }
    }

    void GameLost() {
        if (onGameLost != null) {
            onGameLost.Invoke();
        }
    }


    void PauseGame() {
        Time.timeScale = 0;
        if(onGamePause != null) {
            onGamePause.Invoke();
            isPaused = true;
        }
    }

    void UnPause() {
        Time.timeScale = 1;
        if(onGameResume != null) {
            onGameResume.Invoke();
            isPaused = false;
        }
    }

    void PauseLogic() {
        if (isPaused) {
            UnPause();
        }
        else {
            PauseGame();
        }
    }
}
