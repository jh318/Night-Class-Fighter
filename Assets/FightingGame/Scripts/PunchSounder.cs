using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchSounder : MonoBehaviour
{
    public static PunchSounder instance;

    private int soundIndex;
    private string currentSound;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start ()
    {
        soundIndex = 1;
        StartCoroutine("IndexAdvance");
	}

    IEnumerator IndexAdvance ()
    {
        if (soundIndex > 0 && soundIndex < 9) ++soundIndex;
        else soundIndex = 1;
        yield return new WaitForSeconds(2);
        Debug.Log("soundIndex = " + soundIndex);
    }

    public void GoodHit()
    {  
        if      (soundIndex == 1) currentSound = "Punch1";
        else if (soundIndex == 2) currentSound = "Punch2";
        else if (soundIndex == 3) currentSound = "Punch3";
        else if (soundIndex == 4) currentSound = "Punch4";
        else if (soundIndex == 5) currentSound = "Punch5";
        else if (soundIndex == 6) currentSound = "Punch6";
        else if (soundIndex == 7) currentSound = "Punch7";
        else if (soundIndex == 8) currentSound = "Punch8";
        else if (soundIndex == 9) currentSound = "Punch9";

        //AudioManager.PlayVariedEffect(currentSound, 1.1f, 0);
        AudioManager.PlayEffect(currentSound, 1.1f, 0);

        ++soundIndex;
    }	
}
