using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PowerConsumer {
    void updatePower(bool powered);
}

public class Room : MonoBehaviour {

    public PowerConsumer[] PowerConsumers;

    public shipLight[] Lights;

    public int minPower;

    int p=0;
    public int power=0;
    // Use this for initialization
    void Awake () {
        PowerConsumers = GetComponentsInChildren<PowerConsumer>();
        Lights = GetComponentsInChildren<shipLight>();

        //Debug.Log("Power Consumers:" + this.name + ": " + PowerConsumers.Length);
        //Debug.Log("Lights count in " + this.name + ": " + Lights.Length);
    }

    public int setPower(int power) {
        Debug.LogWarning(name + " Setpower " + power);
        this.power = power;
        updatePower();
        return power;
    }

    public int changePower(bool up) {
        Debug.LogWarning(name + " ChangePower");
        power += up ? 1 : -1;
        updatePower();
        return power;
    }

    public void updatePower() {
        foreach (shipLight l in Lights) {
            l.setPower(power);
        }
        foreach(PowerConsumer p in PowerConsumers) {
            p.updatePower(!(power<minPower));
        }
    }

    public int getMinPower() {
        return minPower;
    }
}
