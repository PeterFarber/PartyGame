using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Menu : MonoBehaviour {


    public GameObject StartText;
    private Joystick_Controller _joystickController;

    // Use this for initialization
    void Awake()
    {
        _joystickController = GameObject.Find("Global_Controller").GetComponent<Joystick_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        int players = _joystickController.PlayerCount();
        int playerOne = _joystickController.PlayerJoystick(0);

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
