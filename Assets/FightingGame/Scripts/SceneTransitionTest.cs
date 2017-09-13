using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransitionTest : MonoBehaviour {

    public Animator leftDoor;
    public Animator rightDoor;

    public string scene;
    public string music;

	void Start ()
    {		
	}
	
	void Update ()
    {
    }

    public void ChangeScene()
    {
        StartCoroutine("ChangeSceneRoutine");    
    }

    IEnumerator ChangeSceneRoutine()
    {
        leftDoor.SetTrigger("CloseLeft"); 
        rightDoor.SetTrigger("CloseRight");
        yield return new WaitForSeconds(3); 
        // LoadScene -> add scenes together; Ex.) TitleScene + MenuScene; 
        SceneManager.LoadScene(scene); 
    }

}
