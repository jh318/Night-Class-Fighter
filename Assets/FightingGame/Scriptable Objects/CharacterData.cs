using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "CharacterInfo/Character", order = 1)]
public class CharacterData : ScriptableObject {

    public string characterName;

    public Sprite mugShot;

    public GameObject charPrefab;

    public GameObject altCharPrefab;
}
