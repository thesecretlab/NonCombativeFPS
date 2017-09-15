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
    public bool isIDS;
    public nodeLink[] links;

    public bool isActive;

    private Color OPEN = Color.grey;
    private Color ACTIVE = Color.cyan;
    private Color CLOSED = new Color(0.3f, 0.3f, 0.3f);
    private Color ERROR = Color.red;

    private HackingUI UI;

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

    public void setUI(HackingUI ui) {
        UI = ui;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (isOpen) {
            if (isFirewall) image.sprite = UI.Firewall;
            else if (isIDS) image.sprite = UI.IDS;
            else if (isExit) {
                image.sprite = UI.systemcore;
                UI.Hacked();
                foreach (nodeLink l in links) {
                    if (l.node.isActive) {
                        l.link.setColor(isFirewall || l.node.isFirewall ? ERROR : ACTIVE);
                    }
                }
                return;
            } else image.color = ACTIVE;
            Debug.Log(UI.name);
            isActive = true;
            foreach (nodeLink l in links) {
                if (l.node.isActive) {
                    l.link.setColor(isFirewall || l.node.isFirewall ? ERROR : ACTIVE);
                } else {
                    if (!isFirewall) {
                        l.node.open();
                        l.link.setColor(OPEN);
                    }
                }
            }
        }
    }

    public Color getColor() {
        if (isFirewall) return ERROR;
        if (isActive) return ACTIVE;
        if (isOpen) return OPEN;
        return CLOSED;
    }

    public void open() {
        if (!isOpen) {
            isOpen = true;
            image.color = OPEN;
        }
    }
}
