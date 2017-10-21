using UnityEngine;
using UnityEngine.EventSystems;

/// 
/// \brief The target for the turret calibration mini-game
/// 
/// \author Jeffrey Albion
/// 
public class Target : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	/// The parent UI
    TurretUI UI;
	/// The rigedbody
    Rigidbody2D rb2d;
	/// The outer bounds of the UI
    public Bounds bound;
	/// Time when force should be applied
    float change;
	/// The transform of the image
    RectTransform rectrans;
	/// If the target has bounced
    bool bouncedx, bouncedy;
    /// 
    /// \brief Sets the UI
    /// 
    /// \param [in] tUI The parent UI
    /// \return No return value
    /// 
    /// \details 
    /// 
    public void setUI(TurretUI tUI) {
        UI = tUI;
    }
    /// 
    /// \brief Used for initialisation
    /// 
    /// \return No return value
    /// 
    /// \details Finds and sets the component variables
    /// 
    void Start() {
        rectrans = GetComponent<RectTransform>();
        rb2d = GetComponent<Rigidbody2D>();
        bound = new Bounds();
        bound.center = transform.position;
        bound.size = new Vector3(UICanvas.getWidth() - rectrans.rect.width, UICanvas.getHeight() - rectrans.rect.height, 1);
    }
    /// 
    /// \brief Triggers when mouse enters
    /// 
    /// \return No return Value
    /// 
    /// \details 
    /// 
    private void OnMouseEnter() {
        //Debug.LogWarning("mouse");
        UI.setOverTarget();
    }
    /// 
    /// \brief Triggers when mouse exits
    /// 
    /// \return No return value
    /// 
    /// \details 
    /// 
    private void OnMouseExit() {
        UI.setLostTarget();
    }
    /// 
    /// \brief Triggers once per frame
    /// 
    /// \return No return value
    /// 
    /// \details 
    /// 
    void Update() {
        Vector3 newVel = rb2d.velocity;
        if (!bound.Contains(new Vector3(transform.position.x,bound.center.y))) {
            //Debug.LogWarning("X Bounce");
            if (!bouncedx) {
                newVel.x *= -1;
                bouncedx = true;
            }
        } else {
            bouncedx = false;
        }
        if (!bound.Contains(new Vector3(bound.center.x, transform.position.y))) {
            //Debug.LogWarning("Y Bounce");
            if (!bouncedy) {
                newVel.y *= -1;
                bouncedy = true;
            }
        } else {
            bouncedy = false;
        }
        rb2d.velocity = newVel;

        float randomX;
        float randomY;

        randomX = Random.Range((float)-10.0, (float)10.0);// Random float is returned and used to update sprite position.
        randomY = Random.Range((float)-10.0, (float)10.0);

        float moveSpeed = 600 + (UI.getAccuracy() * 4f);
        //Debug.Log(moveSpeed);
        if (Time.time >= change) {
            change = Time.time + Random.Range((float)0.5, (float)1.5);
            rb2d.AddForce(new Vector2((randomX * moveSpeed), (randomY * moveSpeed)));
        }
    }
    /// 
    /// \brief Causes the target to bounce
    /// 
    /// \return No return value
    /// 
    /// \details 
    /// 
    void BounceTarget() {
        rb2d.velocity = -rb2d.velocity;
    }
    /// 
    /// \brief Triggers when mouse enters
    /// 
    /// \param [in] eventData Standard mouse data
    /// \return No return value
    /// 
    /// \details 
    /// 
    public void OnPointerEnter(PointerEventData eventData) {
        //Debug.Log("mouse");
        UI.setOverTarget();
    }
    /// 
    /// \brief Triggers when mouse exits
    /// 
    /// \param [in] eventData Standard mouse data
    /// \return No return value
    /// 
    /// \details 
    /// 
    public void OnPointerExit(PointerEventData eventData) {
        //Debug.Log("noMouse");
        UI.setLostTarget();
    }
}
