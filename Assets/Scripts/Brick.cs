using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;

public class Brick : MonoBehaviour {

    public Vector2 coords;
    public int hitPoints;

    protected Light2D light;
    protected TextMeshPro tmp;
    protected BrickManager brickManager;

    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Ball") {
            OnHitByBall();
        }
    }

    public virtual void Init(Vector2 value, int hitPoints) {
        this.hitPoints = hitPoints;
        coords = value;
        light = GetComponentInChildren<Light2D>();
        tmp = GetComponentInChildren<TextMeshPro>();
        tmp.text = hitPoints.ToString();
        brickManager = GetComponentInParent<BrickManager>();
        ChangeColor();
    }

    protected virtual void OnHitByBall() {
        hitPoints--;
        GameplayManager.Instance.IncreaseScore(10);
        UpdateText();
        ChangeColor();
        if (hitPoints <= 0) {
            DestroyLogic();
        }
    }

    protected virtual void DestroyLogic() {
        brickManager.BrickDestroyed(this);
    }

    protected virtual void UpdateText() {
        tmp.text = hitPoints.ToString();
    }

    protected virtual void ChangeColor() {
        if (hitPoints >= 3) {
            light.intensity = 2;
            /* = new Color(230f, 0, 230f);*/
        }
        else if (hitPoints == 2) {
            light.intensity = 1;
            //light.color = new Color(230f, 0f, 230f);
        }
        else {
            light.intensity = 0.7f;
            //light.color = new Color(0f, 0f, 230f);
        }
    }

}
