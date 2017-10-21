using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ControlMap
{
    public Button uiButtonName;

    [System.NonSerialized] public Control control;

    [System.NonSerialized] public Text buttonText;

    public Image controlButtonSprite;

}

public class ButtonRemapUIController : MonoBehaviour {
    public ControlMap[] cm;

    string[] gameButtonName = System.Enum.GetNames(typeof(GameButton));


    private void Start()
    {
        for(int i = 0; i < cm.Length; i++)
        {
            cm[i].buttonText = cm[i].uiButtonName.GetComponentInChildren<Text>();
            Debug.Log(cm[i].buttonText);

            //cm[i].controlButtonSprite = cm[i].uiButtonName.GetComponentInChildren<Image>();
        }

        

        for(int i = 0; i < cm.Length; i++)
        {
            cm[i].buttonText.text = gameButtonName[i];
            Debug.Log(gameButtonName[i]);
            cm[i].control = ControlMapper.player1Mapping[(GameButton)i];
            Debug.Log(i);
            cm[i].controlButtonSprite.sprite = PictureToButtonMapper.c1ButtonMap[cm[i].control.keycode];
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
