using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// \brief An inheritable class to create an interactable object
/// 
public abstract class Interactable : MonoBehaviour {
	/// if the object can currently be interacted with
    public bool active = true;
    /// 
    /// \brief Gets the position of the object
    /// 
    /// \return Returns the position of the object
    /// 
    /// \details 
    /// 
    public Vector3 getPos() {
        return transform.position;
    }
	/// 
	/// \brief Used for initialisation
	/// 
	/// \return No return value
	/// 
	/// \details Adds the object to the list of interactable objects
	/// 
	void Start () {
        this.setup();
        Player.playerObj.addInteractable(this);
	}
    /// 
    /// \brief Gets if the object is interactable
    /// 
    /// \return Returns if the object is interactable
    /// 
    /// \details 
    /// 
    public bool isActive() {
        return active;  
    }
    /// 
    /// \brief Sets if the object is interactable
    /// 
    /// \param [in] active If the object should be interactable
    /// \return No return value
    /// 
    /// \details 
    /// 
    public void setActive(bool active) {
        this.active = active;
    }
    /// 
    /// \brief Used by inheritors for initialisation
    /// 
    /// \return No return value
    /// 
    /// \details 
    /// 
    protected abstract void setup();
    /// 
    /// \brief Used by inheritors when the object is interacted with
    /// 
    /// \return No return value
    /// 
    /// \details 
    /// 
    public abstract void interact();
}