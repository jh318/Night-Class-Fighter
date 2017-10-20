using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

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

        // DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        leftDoor = GameObject.Find("Blast Door Left").GetComponent<Animator>();
        rightDoor = GameObject.Find("Blast Door Right").GetComponent<Animator>();
    }
    public void Title()
    {
        scene = "Title Screen";
        ChangeScene(scene);
    }
    public void MainMenu()
    {
        scene = "Main Menu";
        ChangeScene(scene);
    }
    public void CharacterSelect()
    {
        scene = "Character Select";
        ChangeScene(scene);
    }
    public void FightScene()
    {
        scene = "FightScene";
        ChangeScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeScene(string scene)
    {
        StartCoroutine("ChangeSceneRoutine", scene);
    }

    IEnumerator ChangeSceneRoutine(string scene)
    {
        leftDoor.SetTrigger("CloseLeft");
        rightDoor.SetTrigger("CloseRight");
        yield return new WaitForSeconds(3);
        // LoadScene -> add scenes together; Ex.) TitleScene + MenuScene; 
        SceneManager.LoadScene(scene);
    }
}
