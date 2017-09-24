using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSystem : MonoBehaviour {

    [System.Serializable]
    public struct RoomLevels {
        public Room[] rooms;
        public int[] levels;

        public RoomLevels(Room[] r) {
            this.rooms = r;
            List<int> l = new List<int>();
            foreach (Room i in r) {
                l.Add(0);
            }
            levels = l.ToArray();
        }

        public void crash() {
            foreach (Room r in rooms) {
                r.setPower(0);
            }
        }

        public void restart() {
            for (int i = 0; i < rooms.Length; i++) {
                rooms[i].setPower(levels[i]);
            }
        }

        public int changePower(string room, bool up) {
            for (int i = 0; i < rooms.Length; i++) {
                if (rooms[i].name.ToLower() == room.ToLower()) {
                    if (up) {
                        levels[i] = rooms[i].changePower(up);
                        return levels[i];
                    } else if (levels[i] != 0) {
                        levels[i] = rooms[i].changePower(up);
                        return levels[i];
                    } else {
                        return 0;
                    }
                }
            }
            return -1;
        }

        public int setPower(string room, int pow) {
            for (int i = 0; i < rooms.Length; i++) {
                if (rooms[i].name.ToLower() == room.ToLower()) {
                        levels[i] = rooms[i].setPower(pow);
                        return levels[i];
                }
            }
            return -1;
        }

        public int total() {
            int s = 0;
            foreach (int i in levels) s += i;
            return s;
        }
    }

    public static PowerSystem powersystem;

    void Awake() {
        if (powersystem == null) {
            //Debug.Log("Added Powersystem at: " + gameObject.name);
            powersystem = this;
        } else {
            Debug.LogError("Multiple PowerSystems");
            Destroy(transform.gameObject);
        }
    }

    public RoomLevels rooms;
    public int tick=0;
    public int wait=10;
	// Use this for initialization
	void Start () {
        rooms = new RoomLevels(FindObjectsOfType(typeof(Room)) as Room[]);
        foreach (Room r in rooms.rooms) {
            //Debug.Log(r.name);
        }
    }

    public int restore() {
        rooms.restart();
        return rooms.total();
    }

    public int crash() {
        rooms.crash();
        return 0;
    }

    public int changePower(string room, bool up) {
        int ret = rooms.changePower(room, up);
        ReactorTerminal.reactorObj.setDraw(rooms.total());
        return ret;
    }
    public int setPower(string room, int power) {
        int ret = rooms.setPower(room, power);
        ReactorTerminal.reactorObj.setDraw(rooms.total());
        return ret;
    }
}
