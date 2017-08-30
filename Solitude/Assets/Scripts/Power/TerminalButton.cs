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
        tb.text = name + "\n" + ps.changePower(this.name, eventData.button == PointerEventData.InputButton.Left);
    }
}
