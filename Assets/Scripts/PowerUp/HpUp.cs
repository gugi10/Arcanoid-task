using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUp : PowerupBase
{
    
    public override void DestroyPowerUp() {
        base.DestroyPowerUp();
    }

    public override void Drop(Vector3 spawnPosition) {
        base.Drop(spawnPosition);
    }

    public override void PowerUpEffect() {
        GameplayManager.Instance.IncreaseHealthValue(1);
    }
}
