using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    [SerializeField] float boundaryPoint = 8.6f;
    float speed = 10f;
    float offset;

    void Start() {
        offset = transform.position.x;
    }

    void Update() {
        offset += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        if (offset < -boundaryPoint) {
            offset = -boundaryPoint;
        }
        if (offset > boundaryPoint) {
            offset = boundaryPoint;
        }
        transform.position = new Vector3(offset, transform.position.y, transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null) {
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = collision.contacts[0].point;
            rb.velocity = Vector2.zero;
            float difference = transform.position.x - hitPoint.x;
            if (hitPoint.x < transform.position.x) {
                rb.AddForce(new Vector2(-Mathf.Abs(difference * 100), ball.startingForce));
            }
            else {
                rb.AddForce(new Vector2(Mathf.Abs(difference * 100), ball.startingForce));
            }
        }
    }

    public void ResetPosition() {
        offset = 0;
        transform.position = Vector3.zero;
    }
}
