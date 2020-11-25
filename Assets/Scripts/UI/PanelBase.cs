using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBase : MonoBehaviour {

    [SerializeField] PanelManager manager;

    void Awake() {

    }

    private void Reset() {
        manager = FindObjectOfType<PanelManager>();
    }

    [ContextMenu("Show")]
    public void Show() {
        manager.PanelShowed(this);
        gameObject.SetActive(true);
    }

    [ContextMenu("Hide")]
    public void Hide() {
        gameObject.SetActive(false);
    }
}
