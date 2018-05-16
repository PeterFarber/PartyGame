using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick_Controller : MonoBehaviour {

    // An Array To Store All Players And Thier JoyStick Id.
    private int[] _playersJoystick;

    void Start()
    {
        // Instantiate the _playersJoystick.
        _playersJoystick = new int[4];
        // Initialize all of the values to -1.
        for (int i = 0; i < _playersJoystick.Length; i++)
        {
            _playersJoystick[i] = -1;
        }
    }

    void Awake()
    {
        // Have this object persist through out all scenes.
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        // Check if any controller has press X to join the game.
        JoystickCheck();
    }

    void JoystickCheck()
    {
        // Go through all 16 possible controller ids.
        for (int i = 0; i < 16; i++)
        {

            // Make sure that no other player is already attached to this controller.
            bool con = false;
            for (int c = 0; c < 4; c++)
            {
                if (_playersJoystick[c] == i + 1)
                {
                    con = true;
                }
            }
            if (con)
            {
                continue;
            }
            
            // Check to see if the controller X button was pressed.
            if (Input.GetKeyDown("joystick " + (i + 1) + " button 1"))
            {
                // Add the controller to the the lowest untaken player.
                for (int p = 0; p < 4; p++)
                {
                    if (_playersJoystick[p] == -1)
                    {
                        _playersJoystick[p] = i + 1;
                        break;
                    }
                }
            }
        }
    }

    // Check if the player has a controller.
    public bool CheckPlayer(int p)
    {
        if (_playersJoystick[p] == -1)
        {
            return false;
        }
        return true;
    }

    // Count the ammount of players that have a controller.
    public int PlayerCount()
    {
        int sum = 0;
        for(int i = 0; i < _playersJoystick.Length; i++)
        {
            if (_playersJoystick[i] != -1)
            {
                sum++;
            }
        }
        return sum;
    }

    // Return the controller of a selected player.
    public int PlayerJoystick(int p)
    {
        return _playersJoystick[p];
    }

}
