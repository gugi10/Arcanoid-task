using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour {

    public List<PanelBase> panels;
    public PanelBase startingPanel;

    public void Awake() {
        if (startingPanel != null) startingPanel.Show();
    }

    public void PanelShowed(PanelBase panelBase) {
        foreach (PanelBase panel in panels) {
            if (panel != panelBase) {
                panel.Hide();
            }
        }
    }

    [ContextMenu("Find panels in children")]
    public void FindAllPanelsInChildren() {
        panels = new List<PanelBase>();
        foreach (Transform child in transform) {
            PanelBase panel = child.GetComponent<PanelBase>();
            if (panel != null) {
                panels.Add(panel);
            }
        }
    }
}
