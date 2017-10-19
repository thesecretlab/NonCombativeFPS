using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// 
/// \file PowerSystem.cs
/// 

/// \brief PowerSystem is used to receive and route power to different classes that require it.
public class PowerSystem : MonoBehaviour {

/// 
/// \brief RoomLevels is used to store rooms and their power levels
/// 
    [System.Serializable]
    public struct RoomLevels {
        public Room[] rooms;
        public int[] levels;
        /// 
        /// \brief Constructor For a Roomlevels struct
        /// 
        /// \param [in] r An array of room objects
        /// \param [in] start An integer defining the starting power level for all rooms
        /// \return Returns a RoomLevels object
        /// 
        public RoomLevels(Room[] r,int start) {
            this.rooms = r;
            List<int> l = new List<int>();
            foreach (Room i in r) {
                l.Add(start);
                i.setPower(start);
            }
            levels = l.ToArray();
        }
        /// 
        /// \brief Used to crash the rooms power
        /// 
        /// \return No return value
        /// 
        /// \details Sets the powerlevel of all rooms in the array to 0
        /// 
        public void crash() {
            int i = 0;
            foreach (Room r in rooms) {
                levels[i] = r.setPower(0);
                i++;
            }
        }
        /// 
        /// \brief Used to restore the power to all rooms
        /// 
        /// \return No return value
        /// 
        /// \details Resets the powerlevel of all rooms to the stored value
        /// 
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
        /// 
        /// \brief Gets the minimum power level for the room
        /// 
        /// \param [in] room The name of the room as a String
        /// \return Returns the minimum power as an integer
        /// 
        /// \details The minimum power of the room is the lowest powerlevel that the powerd objects in that room require to function
        /// 
        public int getMin(string room) {
            for (int i = 0; i < rooms.Length; i++) {
                if (rooms[i].name.ToLower() == room.ToLower()) {
                    return rooms[i].getMinPower();
                }
            }
            return -1;
        }
        /// 
        /// \brief Increases or degreases the power to a room
        /// 
        /// \param [in] room The name of the room as a String
        /// \param [in] up Should the power be increased or decreased, true for increase
        /// \param [in] max The maximum total power for all rooms
        /// \return Returns the new powerlevel as an integer
        /// 
        /// \details Will return -1 if the room cannot be found
        /// 
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
        /// 
        /// \brief Sets the powerlevel of a room
        /// 
        /// \param [in] room The room name as a String
        /// \param [in] pow The power level to set the room to
        /// \param [in] max The maximum total power for all rooms
        /// \return Returns the new powerLevel as an integer
        /// 
        /// \details Will return -1 if the room cannot be found
        /// 
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
        /// 
        /// \brief Gets the power level of a room
        /// 
        /// \param [in] room The name of the room as a string
        /// \return Returns the power level of the room
        /// 
        /// \details Will return -1 if the room is not found
        /// 
        public int getPower(string room) {
            for (int i = 0; i < rooms.Length; i++) {
                if (rooms[i].name.ToLower() == room.ToLower()) {
                    return levels[i];
                }
            }
            return -1;
        }
        /// 
        /// \brief Gets the total used power of all rooms
        /// 
        /// \return Returns the total power as an integer
        /// 
        /// \details 
        /// 
        public int total() {
            int s = 0;
            foreach (int i in levels) s += i;
            return s;
        }
    }
	/// Static PowerSystem object used for direct calls
    static PowerSystem powersystem;
	/// The text object that displays the used and available power
    public Text powerText;
	/// The ui used to interact with the power system
    GameObject ui;
	/// An array of TerminalButtons in the ui
    private TerminalButton[] buttons;

    #region Static
    /// 
    /// \brief Static call to the crash function
    /// 
    /// \return Returns 0
    /// 
    /// \details 
    /// 
    public static int crash() {
        return powersystem._crash();
    }
    /// 
    /// \brief Static call to the restore function
    /// 
    /// \return Returns the total amount of used power
    /// 
    /// \details 
    /// 
    public static int restore() {
        return powersystem._restore();
    }
    /// 
    /// \brief Static call to the getRoom function
    /// 
    /// \param [in] room The room name as a string
    /// \return Returns the power level of the room
    /// 
    /// \details Used for finding the power level of a room. Will return -1 if the room is not found
    /// 
    public static int getRoom(string room) {
        return powersystem._getRoom(room);
    }
    /// 
    /// \brief Static call to the changeroom function
    /// 
    /// \param [in] room The name of the room as a string
    /// \param [in] up A boolian stating to increase or decrease the power level
    /// \return Returns the new power level of the room
    /// 
    /// \details Used to increase or decrease the power level of a room. Will return -1 if the room is not found
    /// 
    public static int changeRoom(string room, bool up) {
        return powersystem._changeRoom(room, up);
    }
    /// 
    /// \brief Static call to the setRoom function
    /// 
    /// \param [in] room The name of the room as a String
    /// \param [in] power The new power level of the room
    /// \return Returns the new powerlevel of the room
    /// 
    /// \details Used to set the power level of a room. Will return -1 if the room is not found
    /// 
    public static int setRoom(string room, int power) {
        return powersystem._setRoom(room, power);
    }
    /// 
    /// \brief Static call to the setPower function
    /// 
    /// \param [in] power The new maximum power
    /// \return No return value
    /// 
    /// \details Used to set the maximum total power usage of all rooms
    /// 
    public static void setPower(int power) {
        powersystem._setPower(power);
    }
    /// 
    /// \brief Static function to set the ui
    /// 
    /// \param [in] ui The ui to use
    /// \return No return value
    /// 
    /// \details Sets the UI and finds all needed UI components
    /// 
    public static void setUI(GameObject ui) {
        powersystem.ui = ui;
        powersystem.powerText = ui.transform.FindChild("AvailablePowerVariable").GetComponent<Text>();
        powersystem.buttons = ui.GetComponentsInChildren<TerminalButton>();
    }
    /// 
    /// \brief Static call to the getMin function
    /// 
    /// \param [in] room The room name as a String
    /// \return Returns the minimum power level of the room
    /// 
    /// \details Used to find the minimum required power level of a room. Will return -1 if the room is not found
    /// 
    public static int getMin(string room) {
        return powersystem._getMin(room);
    }
    #endregion
    /// 
    /// \brief Called on initial creation of the object
    /// 
    /// \return No return value
    /// 
    /// \details Used to set the static powerSystem object and make it singular by self deleting if one is already set
    /// 
    void Awake() {
        if (powersystem == null) {
            //Debug.Log("Added Powersystem at: " + gameObject.name);
            powersystem = this;
        } else {
            Debug.LogError("Multiple PowerSystems");
            Destroy(transform.gameObject);
        }
    }
	/// Stores the rooms and power levels
    public RoomLevels rooms;
	/// A room object for the corridors
    public Room corridors;
	/// The current available power
    int aPower;
	/// The current used power
    int uPower;
	
	/// 
	/// \brief Initialization
	/// 
	/// \return No return value
	/// 
	/// \details Finds all rooms and stores them in a RoomLevels, stores corridors separately
	/// 
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
    /// 
    /// \brief Restores the power system
    /// 
    /// \return Returns the total amount of used power
    /// 
    /// \details sets the power level of each room back to the stored value
    /// 
    int _restore() {
        rooms.restart();
        return rooms.total();
    }
    /// 
    /// \brief Crashes the power system
    /// 
    /// \return Returns 0
    /// 
    /// \details Sets the power level of all rooms to 0
    /// 
    int _crash() {
        rooms.crash();
        foreach (TerminalButton button in buttons) {
            button.update();
        }
        return 0;
    }
    /// 
    /// \brief Changes the power level of a room
    /// 
    /// \param [in] room The room name as a string
    /// \param [in] up If the power should be increased or decreased
    /// \return Returns the rooms new power level
    /// 
    /// \details Will return -1 if the room is not found
    /// 
    int _changeRoom(string room, bool up) {
        int ret = rooms.changePower(room, up, aPower);
        updateText();
        return ret;
    }
    /// 
    /// \brief Sets the power level of a room
    /// 
    /// \param [in] room The name of the room as a string
    /// \param [in] power The new power level
    /// \return Returns the new power level of the room
    /// 
    /// \details Will return -1 if the room is not found
    /// 
    int _setRoom(string room, int power) {
        int ret = rooms.setPower(room, power, aPower);
        updateText();
        return ret;
    }
    /// 
    /// \brief Gets the power level of a room
    /// 
    /// \param [in] room The name of the room as a string
    /// \return Returns the power level of the room
    /// 
    /// \details Will return -1 if the room is not found
    /// 
    int _getRoom(string room) {
        return rooms.getPower(room);
    }
    /// 
    /// \brief Updates the available and used power text
    /// 
    /// \return No return value
    /// 
    /// \details 
    /// 
    void updateText() {
        uPower = rooms.total();
        powerText.text = uPower + "/" + aPower;
    }
    /// 
    /// \brief Sets the available power
    /// 
    /// \param [in] power The available power as an integer
    /// \return No return value
    /// 
    /// \details 
    /// 
    void _setPower(int power) {
        aPower = power;
        corridors.setPower(power);
        updateText();
    }
    /// 
    /// \brief Will return the minimum power level of a room
    /// 
    /// \param [in] room The name of the room as a string
    /// \return Returns the minimum power level of the room
    /// 
    /// \details Will return -1 if the room is not found
    /// 
    int _getMin(string room) {
        return rooms.getMin(room);
    }
}
