using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

    public int time;
    bool timesUp = false;
    public Text TimeUI;
    public GameObject TimesUpUI;

	void Start () {
        StartCoroutine("Timer");
	}

    private void Update()
    {
        if (timesUp == true)
        {
            TimesUpUI.gameObject.SetActive(true);
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; time >= i; time--)
        {
            yield return new WaitForSecondsRealtime(1);
            TimeUI.text = "" + time;
        }
        TimeUI.text = "0";
        timesUp = true;
    }
}
