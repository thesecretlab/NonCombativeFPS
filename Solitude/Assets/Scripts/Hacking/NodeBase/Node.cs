using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// Controls the Node behavior of the hacking minigame.
/// Controls the colouring of each node depending on its status.
/// </summary>
/// <remarks>
/// Author: Jeffrey Albion
/// </remark>
public class Node : MonoBehaviour, IPointerClickHandler {
    
	///boolen to store if the firewall is true.
	public bool isFirewall;
	///boolean to store if open is true.
    public bool isOpen;
	///boolean to store if the exit is true.
    public bool isExit;
	///variable to store if the IDS is true.
    public bool isIDS;
	///nodeLink array to store all links.
    public nodeLink[] links;

	///Boolean to sew if the node isactive or not.
	public bool isActive;

    ///variable to store the node sprite.
	Sprite defaultSpt;

	
	///sets the open color to grey.
    private Color OPEN = Color.grey;
	///sets the active color to cyan.
    private Color ACTIVE = Color.cyan;
	///sets the Closed colour to hex value.
    private Color CLOSED = new Color(0.3f, 0.3f, 0.3f);
	///sets the error colour to red.
    private Color ERROR = Color.red;

    ///HackingUI object declaration.
	private HackingUI UI;

	///image variable to store an image for nodes.
    private Image image;

	///gets the image and sets the colour to grey if isOpen is true and hex value if closed.
    void Awake() {
        image = GetComponent<Image>();
        image.color = isOpen ? OPEN : CLOSED;
    }

	///sets the default sprite to be the node image and sets them all to closed colour.
    void Start() {
        defaultSpt = image.sprite;
        foreach (nodeLink l in links) {
            l.link.setColor(CLOSED);
        }
    }
	///sets the UI backgroung.
    public void setUI(HackingUI ui) {
        UI = ui;
    }
	///executes when a node is clicked.
    public void OnPointerClick(PointerEventData eventData) {
        if (isOpen) {
            if (isFirewall)
            {
                setImage(UI.Firewall);
                UI.Firewallclicked();
            }
            else if (isIDS)
            {
                setImage(UI.IDS);
                UI.IDSclicked();

            }
            else if (isExit)
            {
                setImage(UI.systemcore);
                UI.Hacked();
                foreach (nodeLink l in links)
                {
                    if (l.node.isActive)
                    {
                        l.link.setColor(isFirewall || l.node.isFirewall ? ERROR : ACTIVE);
                    }
                }
                return;
            }
            else image.color = ACTIVE;
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
	///runs when a node is closed.
    public void close() {
        isActive = false;
        isOpen = false;
        image.sprite = defaultSpt;
        image.color = CLOSED;
        foreach (nodeLink l in links) {
            if (l.node.isActive) {
                l.link.setColor(OPEN);
            } else {
                l.link.setColor(CLOSED);
            }
        }
    }
	///sets the current sprites image to be the passed in variable.
    public void setImage(Sprite sprite) {
        image.sprite = sprite;
        //image.transform.localScale = new Vector3(1, 1, 1);
        //gameObject.transform.SetAsLastSibling();
    }
	
	///returns the colour value if the node is active, firewall or opened.
    public Color getColor() {
        if (isFirewall) return ERROR;
        if (isActive) return ACTIVE;
        if (isOpen) return OPEN;
        return CLOSED;
    }
	///sets a nodes values to be open. by changing its colour and boolen.
    public void open() {
        if (!isOpen) {
            isOpen = true;
            image.color = OPEN;
        }
    }
}
