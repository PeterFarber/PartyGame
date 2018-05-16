using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRoap_Minigame : MonoBehaviour {

    public GameObject jumpRope;
    public GameObject bar;

    public GameObject[] players;
    public GameObject[] barriers;

    int playerCount = 0;

    public AudioClip win;
    private AudioSource audioSource;

    private GameObject[] hexagon;
    private GameObject selectedHexagon;

    private float countdown = 3.0f;
    private int direction = 1;

    private float yPos;
    private float startPos;

    private float resetTime;
    private bool moving = false;
    private bool running = false;

    private string startButton = "P1_START";


    // Use this for initialization
    void Start () {
        //Set the reset timer to the countDown time.
        resetTime = countdown;
        barriers[0] = bar;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("P1_JUMP"))
        {
            if (CheckFirstPlayer() == true)
            {
                startButton = "P1_START";
            }
            players[0].SetActive(true);

        }

        if (Input.GetButtonDown("P2_JUMP"))
        {
            if (CheckFirstPlayer() == true)
            {
                startButton = "P2_START";
            }
            players[1].SetActive(true);
        }

        if (Input.GetButtonDown("P3_JUMP"))
        {
            if (CheckFirstPlayer() == true)
            {
                startButton = "P3_START";
            }
            players[2].SetActive(true);
        }

        if (Input.GetButtonDown("P4_JUMP"))
        {
            if (CheckFirstPlayer() == true)
            {
                startButton = "P4_START";
            }
            players[3].SetActive(true);
        }
        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            UpdateRope();
        }
    }

    int PlayerCount()
    {
        int sum = 0;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].activeInHierarchy == true)
            {
                sum++;
            }
        }
        return sum;
    }

    private void UpdateRope()
    {
        jumpRope.transform.Rotate(0,5,0);
        foreach (GameObject item in barriers)
        {
            if (item != selectedHexagon)
            {
                item.transform.position = new Vector3(item.transform.position.x, startPos, item.transform.position.z);
            }
        }
    }

    bool CheckFirstPlayer()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].activeSelf == true)
            {
                return false;
            }
        }
        return true;
    }
}
