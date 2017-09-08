using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour, IPointerClickHandler {
    public bool isFirewall;
    public bool isOpen;
    public bool isExit;
    public nodeLink[] links;

    private Color OPEN = Color.grey;
    private Color ACTIVE = Color.cyan;
    private Color CLOSED = new Color(0.3f, 0.3f, 0.3f);
    private Color ERROR = Color.red;

    private HackingTerminal t;

    private Image image;

    void Awake() {
        image = GetComponent<Image>();
        image.color = isOpen ? OPEN : CLOSED;
    }

    void Start() {
        foreach (nodeLink l in links) {
            l.link.setColor(CLOSED);
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (isOpen) {
            if (isExit) {
                transform.parent.SendMessageUpwards("doneHacking");
                return;
            }
            image.color = isFirewall ? ERROR : ACTIVE;
            foreach (nodeLink l in links) {
                if (l.node.isOpen) {
                    l.link.setColor(ACTIVE);
                } else {
                    if (!isFirewall) {
                        l.node.open();
                        l.link.setColor(OPEN);
                    }
                }
            }
        }
    }

    public void setTerminal(HackingTerminal t) {
        this.t = t;
    }

    public void open() {
        isOpen = true;
        image.color = OPEN;
    }
}
