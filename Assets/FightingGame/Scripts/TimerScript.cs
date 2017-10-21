using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

    public static TimerScript instance;

    public int time;
    [HideInInspector]
    public int timeMax;
    public Text TimeUI;
    public GameObject TimesUpUI;
    
    bool timesUp = false;

    void OnEnable(){
        if(instance == null){
            instance = this;
        }
    }

	void Start () {
        timeMax = time;
        StartCoroutine("Timer");
	}

    IEnumerator Timer(){
        time = timeMax;        
        TimeUI.text = time.ToString();
        TimesUpUI.gameObject.SetActive(false);
        timesUp = false;
        yield return new WaitForEndOfFrame();
        for (int i = 0; time >= i; time--){
            yield return new WaitForSecondsRealtime(1);
            TimeUI.text = "" + time;
        }
        TimeUI.text = "0";
        timesUp = true;
        TimesUpUI.gameObject.SetActive(true);
    }
}
