using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PowerConsumer {
    void updatePower(int power);
}

public class Room : MonoBehaviour {

    public PowerConsumer[] PowerConsumers;

    public shipLight[] Lights;

    int p=0;
    public int power=0;
    // Use this for initialization
    void Start () {
        PowerConsumers = GetComponentsInChildren<PowerConsumer>();
        Lights = GetComponentsInChildren<shipLight>();

        //Debug.Log("Power Consumers:" + this.name + ": " + PowerConsumers.Length);
        //Debug.Log("Lights count in " + this.name + ": " + Lights.Length);
    }

    public int setPower(int power) {
        this.power = power;
        updatePower();
        return power;
    }

    public int changePower(bool up) {
        power += up ? 1 : -1;
        updatePower();
        return power;
    }

    public void updatePower() {
        foreach (shipLight l in Lights) {
            l.setPower(power);
        }
        foreach(PowerConsumer p in PowerConsumers) {
            p.updatePower(power);
        }
    }
}
