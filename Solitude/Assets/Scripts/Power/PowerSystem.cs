using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PowerProvider {
    void updateDraw(int draw);
}

public interface PowerConsumer {
    void updatePower(int power);
}

public class PowerSystem : MonoBehaviour {
    Room[] Rooms;
    public int tick=0;
    public int wait=10;
	// Use this for initialization
	void Start () {
        Rooms = FindObjectsOfType(typeof(Room)) as Room[];
    }
	
    public void changePower(string room, bool up) {
        foreach(Room r in Rooms) {
            if (r.name.ToLower() == room.ToLower()) r.changePower(up);
        }
    }

	// Update is called once per frame
	
}
