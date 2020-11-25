using UnityEngine;
using TMPro;

public class HealthText : MonoBehaviour
{
    TextMeshProUGUI tmp;

    void Start() {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        GameplayManager.Instance.onLifeChange += SetText;
        SetText(GameplayManager.Instance.health);
    }

    public void SetText(int value) {
        tmp.text = "Health: " + value.ToString();
    }
}
