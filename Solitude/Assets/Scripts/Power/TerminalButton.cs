using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TerminalButton : MonoBehaviour, IPointerClickHandler {
    PowerSystem ps;
    Text tb;
    string roomName;
    int p = 0;

    void Start() {
        ps = GetComponentInParent<PowerSystem>();
        tb = GetComponent<Text>();
        tb.text = name + "\n" + p;
    }

    public void OnPointerClick(PointerEventData eventData) {
        int np = ps.changePower(this.name, eventData.button == PointerEventData.InputButton.Left);
        tb.text = name + "\n" + np;
        tb.color = np < 1 ? Color.red : Color.blue;
        if (np == -1) {
            Debug.LogError("Error in PowerChange at " + name);
        }
    }
}
