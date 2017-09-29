using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    public GameObject hitBoxHandRight;
    public GameObject hitBoxHandLeft;
    public GameObject hitBoxFootRight;
    public GameObject hitBoxFootLeft;

    private void Start()
    {
        RightHandReturn();
        RightFootReturn();
        LeftHandReturn();
        LeftFootReturn();
    }
    //Right Hand Moves
    public void RightHandStrike ()
    {
        hitBoxHandRight.SetActive(true);
    }
    public void RightHandReturn ()
    {
        hitBoxHandRight.SetActive(false);
    }
    //Left Hand Moves
    public void LeftHandStrike()
    {
        hitBoxHandLeft.SetActive(true);
    }
    public void LeftHandReturn()
    {
        hitBoxHandLeft.SetActive(false);
    }
    //Right Foot Moves
    public void RightFootStrike()
    {
        hitBoxFootRight.SetActive(true);
    }
    public void RightFootReturn ()
    {
        hitBoxFootRight.SetActive(false);
    }
    //Left Foot Moves
    public void LeftFootStrike()
    {
        hitBoxFootLeft.SetActive(true);
    }
    public void LeftFootReturn ()
    {
        hitBoxFootLeft.SetActive(false);
    }

}
