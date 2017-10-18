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

	void Start ()
    {
        AudioManager.AmbientSounds("CrowdLoop");

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
	
	void Update ()
    {
        player1NewHealth = player1Health.healthPointCurr;
        player2NewHealth = player2Health.healthPointCurr;

        if (player1LastHealth > player1NewHealth) Player1Hurt();
        if (player2LastHealth > player2NewHealth) Player2Hurt();

        if (player1Health.healthPointCurr > player2Health.healthPointCurr)
        {
            float dif = (player1Health.healthPointCurr / player1Health.healthPointMax) - (player2Health.healthPointCurr / player2Health.healthPointMax);
            bandWagoners.panStereo = dif;
        }
        else if (player2Health.healthPointMax > player1Health.healthPointMax)
        {
            float dif = (player2Health.healthPointCurr / player2Health.healthPointMax) - (player1Health.healthPointCurr / player1Health.healthPointMax);
            bandWagoners.panStereo = dif;
        }
        else
        {
            bandWagoners.panStereo = Mathf.Lerp(bandWagoners.panStereo, 0, (1 * Time.deltaTime)); 
        }
	}

    void Player1Hurt ()
    {
        if (p2FanVol < 1)
        {
            p2FanVol += 0.1f;
            p2Fans.volume = Mathf.Clamp(p2FanVol, 0, 1.5f);
        }

        player1LastHealth = player1NewHealth;
    }
    void Player2Hurt ()
    {
        if (p1FanVol < 1)
        {
            p1FanVol += 0.1f;
            p1Fans.volume.Equals(p1FanVol);
        }
    }
}
