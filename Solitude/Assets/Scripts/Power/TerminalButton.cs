using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/// 
/// \brief A click-able object for the power UI
/// 
public class TerminalButton : MonoBehaviour, IPointerClickHandler {
	/// The text object displaying on the button
    Text tb;
	/// The room name to display
    public string displayName;
	/// The minimum power of the attached room
    public int minPower;
	/// The colour to use if the room is powered
    public Color powered;
    /// 
    /// \brief Used for initialization
    /// 
    /// \return No return value
    /// 
    /// \details Gets and sets the color form the powerUI object. Gets the text component. Gets the minimum power. Sets the initial display text.
    /// 
    void Start() {
        powered = GetComponentInParent<powerUI>().powered;
        tb = GetComponent<Text>();
        int np = PowerSystem.getRoom(name);
        minPower = PowerSystem.getMin(name);
        tb.text = string.Format("{0}\n{1}/{2}", displayName, np, minPower);
        tb.color = np < minPower ? Color.red : powered;
    }
    /// 
    /// \brief Called every frame
    /// 
    /// \return No return value
    /// 
    /// \details Updates the Displayed text.
    /// 
    public void update() {
        int np = PowerSystem.getRoom(name);
        tb.text = string.Format("{0}\n{1}/{2}", displayName, np, minPower);
        tb.color = np < minPower ? Color.red : powered;
    }
    /// 
    /// \brief Called when the button is clicked
    /// 
    /// \param [in] eventData Unity's auto-passed mouse data
    /// \return No return value
    /// 
    /// \details Will increase the power on click, decrease on any other click type (Right, centre)
    /// 
    public void OnPointerClick(PointerEventData eventData) {
        int np = PowerSystem.changeRoom(this.name, eventData.button == PointerEventData.InputButton.Left);
        tb.text = string.Format("{0}\n{1}/{2}", displayName, np, minPower);
        tb.color = np < minPower ? Color.red : powered;
        if (np == -1) {
            Debug.LogError("Error in PowerChange at " + name);
        }
    }
}
