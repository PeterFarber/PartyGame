using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Join : MonoBehaviour {

    public int _player = -1;

    private Joystick_Controller _joystickController;

	// Use this for initialization
	void Awake () {
        _joystickController = GameObject.Find("Global_Controller").GetComponent<Joystick_Controller>();
    }
	
	// Update is called once per frame
	void Update () {
        bool joined = _joystickController.CheckPlayer(_player-1);
        
        if (joined)
        {
            GetComponent<UnityEngine.UI.Text>().text = "Player " + _player;
        }
	}
}
