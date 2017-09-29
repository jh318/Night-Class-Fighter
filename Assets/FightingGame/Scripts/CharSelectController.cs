using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharSelectController : MonoBehaviour {

    [Header("Selected Button")]
    public Selectable p1;
    public Selectable p2;
    public Selectable defaultButton;

    Selectable newSelection1;
    Selectable newSelection2;

    [Header("Player Selection Indicators")]
    public Transform p1Indicator;
    public Transform p2Indicator;

    [Header("Indicator Animators")]
    public Animator p1Anim;
    public Animator p2Anim;


void Start ()
    {
        p1 = defaultButton;
        p2 = defaultButton;
        newSelection1 = defaultButton;
        newSelection2 = defaultButton;
    }

	void Update ()
    {
        // Get Input p1
        float p1Hori = Input.GetAxisRaw("Horizontal");
        float p1Vert = Input.GetAxisRaw("Vertical");
        Vector2 p1Dir = new Vector2(p1Hori, p1Vert);
        Debug.Log(p1Dir);

        // Get Input p2
        float p2Hori = Input.GetAxisRaw("Horizontal2");
        float p2Vert = Input.GetAxisRaw("Vertical2");
        Vector2 p2Dir = new Vector2(p2Hori, p2Vert);

        // Movement and Selection
        // Move the p1 or p2 indicator depending on the position navigated to && Make sound when moving or selecting;
        // Make a previous newSelection to allow for sounds to play during selection changes;

        // Initialize the Selectable and look for other selectables in the direction of the players' choice;
        Selectable newSelection1 = p1.FindSelectable(p1Dir);
        Selectable newSelection2 = p2.FindSelectable(p2Dir);

        if (newSelection1 != p1 && newSelection1 != null)
        {
            p1 = newSelection1;
            Debug.Log("Moved To The P1 Spot.");
        }

        if (newSelection2 != p2 && newSelection2 != null)
        {
            p2 = newSelection2;
            Debug.Log("Moved To The P2 Spot.");
        }
	}

}
