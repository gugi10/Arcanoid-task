using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : Singleton<BrickManager> {
    public List<Brick> bricks;
    [SerializeField] PowerUpSelector powerUpSelector;
    int bricksDestroyed = 0;
    int startingProbabilityOfDropingPowerUp;

    public void ResetList() {
        bricks = new List<Brick>();
        bricksDestroyed = 0;
    }

    public void BrickDestroyed(Brick brick) {
        bricksDestroyed++;
        bricks.Remove(brick);
        Destroy(brick.gameObject);
        if (bricks.Count <= 0) {
            if (GameplayManager.Instance != null) {
                GameplayManager.Instance.ProceedToNextLevel();
            }
        }
        powerUpSelector.ProbabilityDrop(brick.transform.position);
    }

    public void GetCoordList() {
        
    }

}
