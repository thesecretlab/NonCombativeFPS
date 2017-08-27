using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    PowerConsumer[] PowerConsumers;
    shipLight[] Lights;
    int p=0;
    public int power=0;
    // Use this for initialization
    void Start () {
        PowerConsumers = GetComponentsInChildren<PowerConsumer>();
        Lights = GetComponentsInChildren<shipLight>();
    }

    public void setPower(int power) {
        this.power = power;
        updatePower();
    }

    public void changePower(bool up) {
        power += up ? 1 : -1;
        updatePower();
    }

    public void updatePower() {
        foreach (shipLight l in Lights) {
            l.setPower(power>0);
        }
        foreach(PowerConsumer p in PowerConsumers) {
            p.updatePower(power);
        }
    }
}
