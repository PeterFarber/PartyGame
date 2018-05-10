using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon_Minigame : MonoBehaviour
{

    public GameObject middleOctogon;

    private GameObject[] hexagon;
    private GameObject selectedHexagon;
    private float countdown = 3.0f;
    private int direction = 1;

    private float yPos;
    private float startPos;

    private float resetTime;
    private bool moving = false;
   

    // Use this for initialization
    void Start()
    {
        //Create the field ( Create objects for all floor pieces )
        CreateField();

        //Set the starting position of to the current y position.
        startPos = yPos = middleOctogon.transform.position.y;

        print(yPos);

        //Set the reset timer to the countDown time.
        resetTime = countdown;

        //Select new tile.
        SelectHexagon();

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

    // Update is called once per frame
    void Update()
    {
        if(!moving)
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

    void FixedUpdate()
    {
        if (moving)
        {
            UpdateFloor();
        }
    }

    private void SelectHexagon()
    {
        int randomIndex = (int)Random.Range(0.0f, hexagon.Length-1);
        selectedHexagon = hexagon[randomIndex];
    }

    private void UpdateFloor()
    {
        foreach (GameObject item in hexagon)
        {
            if (item != selectedHexagon)
            {
                item.transform.Translate(Vector3.down * Time.deltaTime * direction * 0.75f);
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
            if (yPos <= startPos-.2f)
            {
                direction *= -1;
            }
        }


    }



}
