using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TerminalButton : MonoBehaviour, IPointerClickHandler {
    Text tb;
    public string displayName;

    public int minPower;

    void Start() {
        tb = GetComponent<Text>();
        tb.text = name.Substring(0, name.IndexOf("_")) + "\n" + PowerSystem.getRoom(name);
    }

    public void OnPointerClick(PointerEventData eventData) {
        int np = PowerSystem.changeRoom(this.name, eventData.button == PointerEventData.InputButton.Left);
        tb.text = string.Format("{0}\n{1}/{2}", displayName, np, minPower);
        tb.color = np < minPower ? Color.red : Color.blue;
        if (np == -1) {
            Debug.LogError("Error in PowerChange at " + name);
        }
    }
}
