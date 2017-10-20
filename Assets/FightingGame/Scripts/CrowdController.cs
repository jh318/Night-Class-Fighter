using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdController : MonoBehaviour
{
    public AudioSource p1Fans;
    public AudioSource p2Fans;
    public AudioSource bandWagoners;

    private float p1FanVol = 0.1f;
    private float p2FanVol = 0.1f;
    private float bandWagonersPan = 0f;

    private HealthController player1Health;
    private HealthController player2Health;

    private int player1NewHealth;
    private int player1LastHealth = 0;

    private int player2NewHealth;
    private int player2LastHealth = 0;

    void Start()
    {
        //AudioManager.AmbientSounds("CrowdLoop");
        p1Fans.volume = 0.1f;
        p1Fans.panStereo = -1;

        p2Fans.volume = 0.1f;
        p2Fans.panStereo = 1;

        bandWagoners.volume = 0.1f;
        bandWagoners.panStereo = 0;

        HealthController[] healthControllers = GameObject.FindObjectsOfType<HealthController>();
        foreach (HealthController health in healthControllers)
        {
            if (health.GetComponent<PlayerController>().playerNumber == 0)
            {
                player1Health = health;
                player1NewHealth = player1Health.healthPointCurr;
            }
            else
            {
                player2Health = health;
                player2NewHealth = player2Health.healthPointCurr;
            }

            player1LastHealth = player1NewHealth;
            player2LastHealth = player2NewHealth;
        }
    }

    void Update()
    {
        player1NewHealth = player1Health.healthPointCurr;
        player2NewHealth = player2Health.healthPointCurr;
        // Debug.Log("One HP" + player1NewHealth);
        // Debug.Log("Two HP" + player2NewHealth);

        if (player1NewHealth > player2NewHealth)
        {
            bandWagoners.panStereo -= 0.1f;
            if (p2Fans.pitch > 0.1f)
            {
                p2Fans.pitch -= 0.1f;
            }
            if (p1Fans.pitch < 1)
            {
                p1Fans.pitch += 0.1f;
            }
        }
        else if (player2NewHealth > player1NewHealth)
        {
            bandWagoners.panStereo += 0.1f;
            if (p1Fans.pitch > 0.1f)
            {
                p1Fans.pitch -= 0.1f;
            }
            if (p2Fans.pitch < 1)
            {
                p2Fans.pitch += 0.1f;
            }
        }

        if (player1NewHealth < player1LastHealth)
        {
            AudioManager.PlayVariedEffect(CrowdCheer(), 0.1f, 1);
        }
        if (player2NewHealth < player2LastHealth)
        {
            AudioManager.PlayVariedEffect(CrowdCheer(), 0.1f, -1);
        }
    }

    string CrowdCheer()
    {
        string namePassed = " ";
        float roll = Random.Range(1, 3);
        int result = (int)roll;

        if (result == 1) namePassed = "Yeah1";
        else if (result == 2) namePassed = "Yeah2";
        else if (result == 3) namePassed = "Yeah3";
        else namePassed = " ";

        return namePassed;
    }
}
