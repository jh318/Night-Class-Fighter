using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuNavigation : MonoBehaviour {

    [Header("Selected Buttons")]
    public Selectable currentSelection;
    public Selectable defaultSelection;
    Selectable nextSelectable;

    [Header("Indicator")]
    public Transform indicator;
    public Animator indicatorAnim;

	void Start ()
    {
		
	}
	void Update ()
    {
		
	}
}
