using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataGenerator : MonoBehaviour {

    [SerializeField] MapGenerator mapGenerator;
    [SerializeField] BrickManager brickManager;
    [SerializeField] GameObject continueButton;

    private void Start() {
        ShowContinueButton();
    }

    public void SaveTheProgress() {
        Debug.Log("save the progress");
        StoredData data = new StoredData();
        data.mapHeight = mapGenerator.maxBricksAtYaxis;
        data.mapWidth = mapGenerator.maxBricksAtXaxis;
        data.currentScore = GameplayManager.Instance.score;
        data.hp = GameplayManager.Instance.health;
        data.coords = new int[mapGenerator.maxBricksAtXaxis, mapGenerator.maxBricksAtYaxis];
        for (int y = 0; y < mapGenerator.maxBricksAtYaxis; y++) {
            for (int x = 0; x < mapGenerator.maxBricksAtXaxis; x++) {
                data.coords[x, y] = 0;
            }
        }
        foreach (Brick brick in brickManager.bricks) {
            data.coords[(int)brick.coords.x, (int)brick.coords.y] = brick.hitPoints;
        }
        data.seed = mapGenerator.generatedMaps[mapGenerator.generatedMaps.Count - 1].seed;
        data.fillPercent = mapGenerator.generatedMaps[mapGenerator.generatedMaps.Count - 1].fillPercent;
        data.canBeContinued = GameplayManager.Instance.health > 0 && GameplayManager.Instance.isStarted;
        Debug.Log(data.canBeContinued);
        SaveData.SaveGame(data);
    }

    public void SaveTheHighestScore() {
        HighestScore previousScore = SaveData.LoadScore();
        if (previousScore != null) {
            if (previousScore.highestScore < GameplayManager.Instance.highestScore) {
                HighestScore newHighestScore = new HighestScore();
                newHighestScore.highestScore = GameplayManager.Instance.highestScore;
                SaveData.SaveHighetScore(newHighestScore);
            }
        }
    }

    public void LoadTheProgress() {
        Debug.Log("try to load");
        StoredData data = SaveData.LoadGame();
        if (data != null) {
            Debug.Log("not null");
            if (data.canBeContinued) {
                Debug.Log("can load");
                continueButton.SetActive(true);

                mapGenerator.maxBricksAtXaxis = data.mapWidth;
                mapGenerator.maxBricksAtYaxis = data.mapHeight;
                GameplayManager.Instance.SetScoreValue(data.currentScore);
                GameplayManager.Instance.SetHealthValue(data.hp);
                mapGenerator.PositionAllBricks(data.coords);
                MapGenerator.MapData mapData = new MapGenerator.MapData();
                mapData.seed = data.seed;
                mapData.fillPercent = data.fillPercent;
                mapGenerator.generatedMaps.Add(mapData);
            }
            else {
                continueButton.SetActive(false); 
            }
        }
        else {
            continueButton.SetActive(false);
        }
    }

    public int LoadHighestScore() {
        HighestScore score = SaveData.LoadScore();
        if (score != null) {
            return score.highestScore;
        }
        else {
            return 0;
        }
    }

    void ShowContinueButton() {
        StoredData data = SaveData.LoadGame();
        if (data != null) {
            continueButton.SetActive(data.canBeContinued);
        }
        else {
            continueButton.SetActive(false);
        }
    }
}
