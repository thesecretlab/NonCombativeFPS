using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class TerminalButton : MonoBehaviour, IPointerClickHandler {
    Text tb;
    public string displayName;

    public int minPower;
    public Color powered;

    static Color rgbColor(int r,int g,int b) {
        Color ret = new Color(r / 255.0f, g / 255.0f, b / 255.0f,1.0f);
        Debug.Log(ret);
        return ret;
    }

    void Start() {
        powered = GetComponentInParent<powerUI>().powered;
        tb = GetComponent<Text>();
        int np = PowerSystem.getRoom(name);
        minPower = PowerSystem.getMin(name);
        tb.text = string.Format("{0}\n{1}/{2}", displayName, np, minPower);
        tb.color = np < minPower ? Color.red : powered;
    }

    public void update() {
        tb.text = name.Substring(0, name.IndexOf("_")) + "\n" + PowerSystem.getRoom(name);
    }

    public void OnPointerClick(PointerEventData eventData) {
        int np = PowerSystem.changeRoom(this.name, eventData.button == PointerEventData.InputButton.Left);
        tb.text = string.Format("{0}\n{1}/{2}", displayName, np, minPower);
        tb.color = np < minPower ? Color.red : powered;
        if (np == -1) {
            Debug.LogError("Error in PowerChange at " + name);
        }
    }
}
