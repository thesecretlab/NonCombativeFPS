using System;
using UnityEngine;
using UnityEngine.UI;

/// 
/// \brief Default scrollbar script
/// 
[RequireComponent(typeof(Button))]
public class ScrollbarIncrementer : MonoBehaviour
{
	///sets a scrollbar variable for type target.
    public Scrollbar Target;
	///button variable.
    public Button TheOtherButton;
    ///float variable to store the stpe.
	public float Step = 0.1f;

	
	///increments target value.
    public void Increment()
    {
        if (Target == null || TheOtherButton == null) throw new Exception("Setup ScrollbarIncrementer first!");
        Target.value = Mathf.Clamp(Target.value + Step, 0, 1);
        GetComponent<Button>().interactable = Target.value != 1;
        TheOtherButton.interactable = true;
    }

	///decrements target value.
    public void Decrement()
    {
        if (Target == null || TheOtherButton == null) throw new Exception("Setup ScrollbarIncrementer first!");
        Target.value = Mathf.Clamp(Target.value - Step, 0, 1);
        GetComponent<Button>().interactable = Target.value != 0; ;
        TheOtherButton.interactable = true;
    }
}
