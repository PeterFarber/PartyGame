using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon_Minigame : MonoBehaviour
{

    public GameObject middleOctogon;
    public GameObject flag;

    public GameObject[] players;

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
    void Start()
    {

        audioSource = this.GetComponent<AudioSource>();

        //Create the field ( Create objects for all floor pieces )
        CreateField();

        //Set the starting position of to the current y position.
        startPos = yPos = middleOctogon.transform.position.y;

        //print(yPos);

        //Set the reset timer to the countDown time.
        resetTime = countdown;

    }

    //Create Field
    void CreateField()
    {
        hexagon = new GameObject[7];
        hexagon[0] = middleOctogon;
        hexagon[1] = Instantiate(middleOctogon, middleOctogon.transform.position + new Vector3(0.866025f, 0, 1.5f), middleOctogon.transform.rotation);
        hexagon[1].GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        hexagon[2] = Instantiate(middleOctogon, middleOctogon.transform.position + new Vector3(-0.866025f, 0, -1.5f), middleOctogon.transform.rotation);
        hexagon[2].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        hexagon[3] = Instantiate(middleOctogon, middleOctogon.transform.position + new Vector3(-0.866025f, 0, 1.5f), middleOctogon.transform.rotation);
        hexagon[3].GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        hexagon[4] = Instantiate(middleOctogon, middleOctogon.transform.position + new Vector3(0.866025f, 0, -1.5f), middleOctogon.transform.rotation);
        hexagon[4].GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
        hexagon[5] = Instantiate(middleOctogon, middleOctogon.transform.position + new Vector3(-1.7320508075688772935274463415059f, 0, 0), middleOctogon.transform.rotation);
        hexagon[5].GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        hexagon[6] = Instantiate(middleOctogon, middleOctogon.transform.position + new Vector3(1.7320508075688772935274463415059f, 0, 0), middleOctogon.transform.rotation);
        hexagon[6].GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);

    }

    int PlayerCount()
    {
        int sum = 0;
        for(int i = 0; i < players.Length; i++)
        {
            if(players[i].activeInHierarchy == true)
            {
                sum++;
            }
        }
        return sum;
    }

    IEnumerator playEngineSound()
    {
        if (running)
        {
            playerCount = PlayerCount();
            print(playerCount);
            if (playerCount == 1)
            {
                audioSource.clip = win;
                audioSource.Play();
                running = false;
                yield return new WaitForSeconds(audioSource.clip.length);
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

        // Update is called once per frame
    void Update()
    {
        StartCoroutine(playEngineSound());
  
        if (Input.GetButtonDown(startButton))
        {
            //Set game is running equal to true!
            running = true;

            //Select new tile.
            SelectHexagon();

            //Get Player Count.
            playerCount = PlayerCount();

        }

        if (Input.GetButtonDown("P1_JUMP"))
        {
            if(CheckFirstPlayer() == true)
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

        if (!moving && running)
        {
            //Update the countDown timer.
            countdown -= Time.deltaTime;

            //If countDown is finished.
            if (countdown <= 0.0f)
            {

                //Set moving to true.
                moving = true;
                //Reset the countDown
                countdown = resetTime;
                //Select a random resetTime for next countDown.
                resetTime = Random.Range(1.0f, 3.0f);
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

    void FixedUpdate()
    {
        if (moving)
        {
            UpdateFloor();
        }
    }

    private void SelectHexagon()
    {
        int randomIndex = (int)Random.Range(0.0f, hexagon.Length);
        switch (randomIndex)
        {
            case 0:
                flag.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                break;
            case 1:
                flag.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                break;
            case 2:
                flag.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                break;
            case 3:
                flag.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                break;
            case 4:
                flag.GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
                break;
            case 5:
                flag.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                break;
            case 6:
                flag.GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
                break;
        }
        selectedHexagon = hexagon[randomIndex];
    }

    private void UpdateFloor()
    {
        foreach (GameObject item in hexagon)
        {
            if (item != selectedHexagon)
            {
                item.transform.Translate(Vector3.down * Time.deltaTime * direction * 0.5f);
            }
        }
        if (direction == -1)
        {
            yPos += Time.deltaTime * 0.1f;
            if (yPos >= startPos)
            {
                direction *= -1;
                moving = false;
                yPos = startPos;
                foreach (GameObject item in hexagon)
                {
                    if (item != selectedHexagon)
                    {
                        item.transform.position = new Vector3(item.transform.position.x, startPos, item.transform.position.z);
                    }
                }
                SelectHexagon();
            }
        }
        else
        {
            yPos -= Time.deltaTime * 0.1f;
            if (yPos <= startPos-.5f)
            {
                direction *= -1;
            }
        }


    }



}
