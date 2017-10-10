using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSystem : MonoBehaviour {

    [System.Serializable]
    public struct RoomLevels {
        public Room[] rooms;
        public int[] levels;

        public RoomLevels(Room[] r,int start) {
            this.rooms = r;
            List<int> l = new List<int>();
            foreach (Room i in r) {
                l.Add(start);
                i.setPower(start);
            }
            levels = l.ToArray();
        }

        public void crash() {
            int i = 0;
            foreach (Room r in rooms) {
                levels[i] = r.setPower(0);
                i++;
            }
        }

        public void restart() {
            for (int i = 0; i < rooms.Length; i++) {
                rooms[i].setPower(levels[i]);
            }
        }

        /*public void randomReduce(int max) {
            while (total() > max) {
                int i = Random.Range(0, levels.Length);
                levels[i] = rooms[i].changePower(false);
            }
        }*/

        public int getMin(string room) {
            for (int i = 0; i < rooms.Length; i++) {
                if (rooms[i].name.ToLower() == room.ToLower()) {
                    return rooms[i].getMinPower();
                }
            }
            return -1;
        }

        public int changePower(string room, bool up, int max) {
            for (int i = 0; i < rooms.Length; i++) {
                if (rooms[i].name.ToLower() == room.ToLower()) {
                    if (up) {
                        if (total() < max) {
                            levels[i] = rooms[i].changePower(up);
                        }
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

        public int setPower(string room, int pow, int max) {
            for (int i = 0; i < rooms.Length; i++) {
                if (rooms[i].name.ToLower() == room.ToLower()) {
                    if (total() - levels[i] + pow < max) {
                        levels[i] = rooms[i].setPower(pow);
                    }
                    return levels[i];
                }
            }
            return -1;
        }

        public int getPower(string room) {
            for (int i = 0; i < rooms.Length; i++) {
                if (rooms[i].name.ToLower() == room.ToLower()) {
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

    static PowerSystem powersystem;
    public Text powerText;
    GameObject ui;

    #region Static
    public static int crash() {
        return powersystem._crash();
    }
    public static int restore() {
        return powersystem._restore();
    }
    public static int getRoom(string room) {
        return powersystem._getRoom(room);
    }
    public static int changeRoom(string room, bool up) {
        return powersystem._changeRoom(room, up);
    }
    public static int setRoom(string room, int power) {
        return powersystem._setRoom(room, power);
    }
    public static void setPower(int power) {
        powersystem._setPower(power);
    }
    public static void setUI(GameObject ui) {
        powersystem.ui = ui;
        powersystem.powerText = ui.transform.FindChild("AvailablePowerVariable").GetComponent<Text>();
    }
    public static int getMin(string room) {
        return powersystem._getMin(room);
    }
    #endregion

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
    public Room corridors;
    public int tick=0;
    public int wait=10;
    int aPower;
    int uPower;
	// Use this for initialization
	void Start () {
        List<Room> rooms = new List<Room>();
        foreach (Room room in FindObjectsOfType(typeof(Room))) {
            if (!room.name.ToLower().Equals("corridors")) {
                rooms.Add(room);
            } else {
                corridors = room;
            }
        }
        this.rooms = new RoomLevels(rooms.ToArray(),0);
    }

    int _restore() {
        rooms.restart();
        return rooms.total();
    }

    int _crash() {
        rooms.crash();
        return 0;
    }

    int _changeRoom(string room, bool up) {
        int ret = rooms.changePower(room, up, aPower);
        updateText();
        return ret;
    }
    int _setRoom(string room, int power) {
        int ret = rooms.setPower(room, power, aPower);
        updateText();
        return ret;
    }
    int _getRoom(string room) {
        return rooms.getPower(room);
    }
    void updateText() {
        uPower = rooms.total();
        powerText.text = uPower + "/" + aPower;
    }
    void _setPower(int power) {
        aPower = power;
        corridors.setPower(power);
        updateText();
    }
    int _getMin(string room) {
        return rooms.getMin(room);
    }
}
