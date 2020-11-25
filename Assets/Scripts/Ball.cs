using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour {

    public enum BallState { idle, moving, lost };
    public float startingForce = 250;

    [SerializeField] float offsetFromPaddle = .25f;
    [SerializeField] Paddle paddle;

    BallState state = BallState.idle;
    Rigidbody2D rigidbody2D;

    void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (state == BallState.idle) {
            transform.position = paddle.transform.position + new Vector3(0, offsetFromPaddle, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space) && state == BallState.idle) {
            state = BallState.moving;
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.AddForce(new Vector2(0, startingForce));
        }
        if (BallState.moving == state && rigidbody2D.velocity.y == 0) {
            rigidbody2D.AddForce(new Vector2(0, -20));
        }
    }

    public void SetToLoseState() {
        SetState(BallState.lost);
        gameObject.SetActive(false);
    }

    public void ResetBall() {
        if(BallState.lost == state) {
            gameObject.SetActive(true);
        }
        transform.position = paddle.transform.position + new Vector3(0, offsetFromPaddle, 0);
        state = BallState.idle;
    }

    public void SetState(BallState state) {
        this.state = state;
    }
}
