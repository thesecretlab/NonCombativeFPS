using UnityEngine;
using UnityEngine.EventSystems;

public class Target : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    TurretUI UI;
    Rigidbody2D rb2d;
    public Bounds bound;
    float change;
    RectTransform rectrans;
    bool bouncedx, bouncedy;
    public void setUI(TurretUI tUI) {
        UI = tUI;
    }
    void Start() {
        rectrans = GetComponent<RectTransform>();
        rb2d = GetComponent<Rigidbody2D>();
        bound = new Bounds();
        bound.center = transform.position;
        bound.size = new Vector3(UICanvas.getWidth() - rectrans.rect.width, UICanvas.getHeight() - rectrans.rect.height, 1);
    }
    private void OnMouseEnter() {
        Debug.LogWarning("mouse");
        UI.setOverTarget();
    }

    private void OnMouseExit() {
        UI.setLostTarget();
    }

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

    void BounceTarget() {
        rb2d.velocity = -rb2d.velocity;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //Debug.Log("mouse");
        UI.setOverTarget();
    }

    public void OnPointerExit(PointerEventData eventData) {
        //Debug.Log("noMouse");
        UI.setLostTarget();
    }
}
