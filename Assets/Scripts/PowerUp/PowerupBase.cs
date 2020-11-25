using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerupBase : MonoBehaviour
{
    [SerializeField] protected float dropSpeed = 5f;
    protected bool isFalling;
    protected float yPosition;
    void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("collison " + collision.name);
        if(collision.gameObject.tag == "Paddle") {
            PowerUpEffect();
            DestroyPowerUp();
        }
        if (collision.gameObject.tag == "PowerUpKiller") {
            DestroyPowerUp();

        }
    }

    private void Update() {
        if (isFalling) {
            //yPosition -= Time.deltaTime * dropSpeed;
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * dropSpeed, transform.position.z);
        }
    }

    public virtual void Drop(Vector3 spawnPosition) {
        transform.position = spawnPosition;
        isFalling = true;
    }

    public virtual void DestroyPowerUp() {
        Destroy(gameObject);
    }

    public abstract void PowerUpEffect();
}
