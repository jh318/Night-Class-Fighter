using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    SceneDirector instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void Title ()
    {
        SceneManager.LoadScene("Title Screen");
    }
    public void MainMenu ()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
