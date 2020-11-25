﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        Ball ball = collision.GetComponent<Ball>();
        if (ball != null) {
            GameplayManager.Instance.LostBall();
            ball.ResetBall();
        }
    }
}