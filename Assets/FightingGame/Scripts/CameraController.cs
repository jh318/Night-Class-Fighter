using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject[] players;
    Vector3 player1;
    Vector3 player2;
    public Vector3 offset;
    public float height;
    Vector3 center;
    Camera camera;
    float distanceMax = 1.9f;
    public float distance;
    Vector3 cameraZ;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerController>();
        }

        camera = Camera.main;
        cameraZ = camera.transform.position;

    }

    private void Update()
    {
         UpdateCamera();
    }

    public void UpdateCamera()
    {
        player1 = players[0].transform.position;
        player2 = players[1].transform.position;
        distance = Vector3.Distance(player1.normalized, player2.normalized);
        center = (player1 + player2) / 2;

        if (distance <= distanceMax)
        {
            camera.transform.position = new Vector3(center.x, camera.transform.position.y, camera.transform.position.z) + offset;
        }
    }
}
