using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighestScoreText : MonoBehaviour
{
    TextMeshProUGUI tmp;

    void Start() {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        GameplayManager.Instance.onHighestScoreSet += SetText;
        SetText(GameplayManager.Instance.highestScore);
    }

    public void SetText(int value) {
        tmp.text = "Highest score: " + GameplayManager.Instance.highestScore.ToString();
    }
}
