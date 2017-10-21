using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCatcher : MonoBehaviour {

    
    //private void Update()
    //{
    //  for(int i = 1; i < 2; i++)
    //  {
    //        for(int j = 0; j < 20; j++)
    //        {
    //            if(Input.GetKeyDown("joystick " + i + " button " + j))
    //            {
    //                Debug.Log("joystick " + i + " button " + j + " pressed.");
    //            }


    //        }

    //        for (int j = 1; j < 11; j++)
    //        {
    //            if (j == 8) continue;
    //            if (Input.GetAxis("joystick " + i + " axis " + j) > 0.9f || Input.GetAxis("joystick " + i + " axis " + j) < -0.9f)
    //            {
    //                Debug.Log("joystick " + i + " axis " + j + " moved");
    //            }
    //        }
    //  }


    //}

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            Debug.Log("Detected key code: " + e.keyCode);
        }
    }


}
