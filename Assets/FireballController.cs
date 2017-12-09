using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour {

	void OnTriggerEnter(Collider c){
		if(c.gameObject.GetComponent<PlayerController>()){
			c.GetComponent<HealthController>().healthPointCurr--;
			c.GetComponent<HealthController>().healthBarUI.size = (float)c.GetComponent<HealthController>().healthPointCurr/(float)c.GetComponent<HealthController>().healthPointMax;
			Destroy(this.gameObject);
		}
	}
	
}
