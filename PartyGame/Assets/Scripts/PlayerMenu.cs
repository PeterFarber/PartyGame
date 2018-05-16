using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMenu : MonoBehaviour {


    public GameObject StartText;
    private GameObject _persitedController;

    // Use this for initialization
    void Awake()
    {
        _persitedController = GameObject.Find("Persited_Controller");
    }

    // Update is called once per frame
    void Update()
    {
        int players = _persitedController.GetComponent<Persist>().PlayerCount();
        int playerOne = _persitedController.GetComponent<Persist>().PlayerJoystick(0);

        if (players >= 2){
            StartText.GetComponent<UnityEngine.UI.Text>().color = Color.white;
            if(Input.GetKeyDown("joystick " + playerOne + " button 9"))
            {
                SceneManager.LoadScene("HexagonHeat");
            }
        }
        else
        {
            StartText.GetComponent<UnityEngine.UI.Text>().color = Color.black;
        }
    }
}
