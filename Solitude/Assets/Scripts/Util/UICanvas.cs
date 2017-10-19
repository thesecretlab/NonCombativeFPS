using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// 
/// \brief A Class for The UI canvas
/// 
public class UICanvas : MonoBehaviour {
	/// A static reference to the UICanvas
    public static UICanvas Canvas;

	/// 
	/// \brief Used for initialization
	/// 
	/// \return No return value
	/// 
	/// \details Sets the static UICanvas. Will self remove if already set
	/// 
	void Awake () {
        if (Canvas == null) {
            Canvas = this;
        } else {
            Destroy(transform.gameObject);
        }
	}
    /// 
    /// \brief Gets the width of the canvas
    /// 
    /// \return Returns the width of the canvas
    /// 
    /// \details 
    /// 
    public static float getWidth() {
        //Debug.Log(Canvas.GetComponent<RectTransform>().rect.width);
        return Canvas.GetComponent<RectTransform>().rect.width;
    }
    /// 
    /// \brief Gets the height of the canvas
    /// 
    /// \return Returns the height of the canvas
    /// 
    /// \details 
    /// 
    public static float getHeight() {
        //Debug.Log(Canvas.GetComponent<RectTransform>().rect.height);
        return Canvas.GetComponent<RectTransform>().rect.height;
    }
}
