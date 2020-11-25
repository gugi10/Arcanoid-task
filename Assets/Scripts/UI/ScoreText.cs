using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    TextMeshProUGUI tmp;

    void Start() {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        GameplayManager.Instance.onScoreChange += SetText;
        SetText(GameplayManager.Instance.score);
    }

    public void SetText(int value) {
        tmp.text = "Score: " + value.ToString();
    }
}
