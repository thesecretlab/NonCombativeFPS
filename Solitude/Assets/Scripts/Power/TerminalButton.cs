using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TerminalButton : MonoBehaviour, IPointerClickHandler {
    Text tb;
    string roomName;

    void Start() {
        tb = GetComponent<Text>();
        tb.text = name.Substring(0, name.IndexOf("_")) + "\n" + PowerSystem.getPower(name);
    }

    public void OnPointerClick(PointerEventData eventData) {
        int np = PowerSystem.changePower(this.name, eventData.button == PointerEventData.InputButton.Left);
        tb.text = name.Substring(0,name.IndexOf("_")) + "\n" + np;
        tb.color = np < 1 ? Color.red : Color.blue;
        if (np == -1) {
            Debug.LogError("Error in PowerChange at " + name);
        }
    }
}
