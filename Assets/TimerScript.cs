using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

    public int time;
    bool timesUp = false;
    public Text TimeUI;

	void Start () {
        StartCoroutine("Timer");
	}

    IEnumerator Timer()
    {
        yield return new WaitForEndOfFrame();
        for (int i = time; i >= 0; i--)
        {
            yield return new WaitForSecondsRealtime(1);
            TimeUI.text = "" + i;
        }
        timesUp = true;
    }
}
