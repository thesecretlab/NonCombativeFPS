using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TerminalButton : MonoBehaviour, IPointerClickHandler {
    PowerSystem ps;
    Text tb;
    string roomName;
    int p = 1;

    void Start() {
        ps = PowerSystem.powersystem;
        tb = GetComponent<Text>();
        int np = ps.setPower(this.name, p);
        tb.text = name.Substring(0, name.IndexOf("_")) + "\n" + np;
        tb.color = np < 1 ? Color.red : Color.blue;
    }

    public void OnPointerClick(PointerEventData eventData) {
        int np = ps.changePower(this.name, eventData.button == PointerEventData.InputButton.Left);
        tb.text = name.Substring(0,name.IndexOf("_")) + "\n" + np;
        tb.color = np < 1 ? Color.red : Color.blue;
        if (np == -1) {
            Debug.LogError("Error in PowerChange at " + name);
        }
    }
}
