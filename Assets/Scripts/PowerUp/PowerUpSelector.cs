using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSelector : MonoBehaviour
{
    [SerializeField] List<GameObject> availablePowerUpsPrefabs = new List<GameObject>();
    [SerializeField] float startingProbability = 10f;
    int increasingProbability = 0;
    public void ProbabilityDrop(Vector3 position) {
        increasingProbability++;
        if(Random.Range(0,100)< startingProbability + increasingProbability) {
            increasingProbability = 0;
            GameObject go = Instantiate(availablePowerUpsPrefabs[SelectRandomPowerUp()], transform);
            PowerupBase powerUp = go.GetComponent<PowerupBase>();
            powerUp.Drop(position);
        }
    }

    int SelectRandomPowerUp() {
        return Random.Range(0, availablePowerUpsPrefabs.Count);
    }
}
