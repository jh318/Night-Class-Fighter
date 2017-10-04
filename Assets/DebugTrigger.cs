using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider c){
		Debug.Log("Here");
		if(c.gameObject.GetComponentInParent<PlayerController>())
			Debug.Log("Trigger");
	}
}
