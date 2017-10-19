using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// 
/// \brief Interface allowing an object to require power
/// 
public interface PowerConsumer {
    /// 
    /// \brief Tells the object if it is powered
    /// 
    /// \param [in] powered Boolean for is powered
    /// \return No return value
    /// 
    /// \details 
    /// 
    void updatePower(bool powered);
}

/// 
/// \brief Provides an interface between the powerSystem and powered objects
/// 
public class Room : MonoBehaviour {
	/// An array of objects requiring power
    public PowerConsumer[] PowerConsumers;
	/// An array of power-able light objects in the room
    public shipLight[] Lights;
	/// The minimum power required for objects in the room to be usable. Set in the inspector
    public int minPower;
	/// The current amount of power provided to the room
    public int power=0;
    /// 
    /// \brief Initialization of the room
    /// 
    /// \return No return value
    /// 
    /// \details Finds all child objects that are power-able and all light objects.
    /// 
    void Awake () {
        PowerConsumers = GetComponentsInChildren<PowerConsumer>();
        Lights = GetComponentsInChildren<shipLight>();

        //Debug.Log("Power Consumers:" + this.name + ": " + PowerConsumers.Length);
        //Debug.Log("Lights count in " + this.name + ": " + Lights.Length);
    }
    /// 
    /// \brief Sets the power level of the room
    /// 
    /// \param [in] power The new power level
    /// \return Returns the new power level
    /// 
    /// \details Will update all powered objects in the room.
    /// 
    public int setPower(int power) {
        //Debug.LogWarning(name + " Setpower " + power);
        this.power = power;
        updatePower();
        return power;
    }
    /// 
    /// \brief Changes the power level of the room
    /// 
    /// \param [in] up If the power level should be increased of decreased
    /// \return Returns the new power level of the room
    /// 
    /// \details 
    /// 
    public int changePower(bool up) {
        //Debug.LogWarning(name + " ChangePower");
        power += up ? 1 : -1;
        updatePower();
        return power;
    }
    /// 
    /// \brief Updates all power-able objects in the room
    /// 
    /// \return No return value
    /// 
    /// \details Tells all power-able objects if the minimum power level is provided
    /// 
    public void updatePower() {
        foreach (shipLight l in Lights) {
            l.setPower(power);
        }
        foreach(PowerConsumer p in PowerConsumers) {
            p.updatePower(!(power<minPower));
        }
    }
    /// 
    /// \brief Gets the minimum power for the room
    /// 
    /// \return Returns the minimum power
    /// 
    /// \details 
    /// 
    public int getMinPower() {
        return minPower;
    }
}
