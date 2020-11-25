using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBrick : Brick {


    public override void Init(Vector2 value, int hitPoints) {
        base.Init(value, hitPoints);
    }

    protected override void DestroyLogic() {
        base.DestroyLogic();
        DropPowerUp();
    }



    void DropPowerUp() {

    }

}
