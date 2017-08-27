using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TerminalButton : MonoBehaviour, IPointerClickHandler {
    PowerSystem ps;
    void Start() {
        ps = GetComponentInParent<PowerSystem>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left)
            ps.changePower(this.name, true);
        else if (eventData.button == PointerEventData.InputButton.Right)
            ps.changePower(this.name, false);
    }
}
