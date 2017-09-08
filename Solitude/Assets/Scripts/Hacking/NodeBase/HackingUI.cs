using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingUI : MonoBehaviour {
    HackingTerminal t;
    void doneHacking() {
        t.onFix();
    }
    public void setTerminal(HackingTerminal ht) {
        Debug.Log("SETT");
        t = ht;
    }
}
