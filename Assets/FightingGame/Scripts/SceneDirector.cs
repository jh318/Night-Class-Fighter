using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    public static SceneDirector instance;

    public Animator leftDoor;
    public Animator rightDoor;

    string scene;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void Title ()
    {
        scene = "Title Screen";
        ChangeScene();
    }
    public void MainMenu ()
    {
        scene = "Main Menu";
        ChangeScene();
    }
    public void CharacterSelect()
    {
        scene = "Character Select";
        ChangeScene();
    }
    public void FightScene()
    {
        scene = "FightScene";
        ChangeScene();
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
