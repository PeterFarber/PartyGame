using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persist : MonoBehaviour {

    public int[] _playersJoystick;

    private string _startButton;

    void Start()
    {
        _startButton = "";

        //Initlialize all of the _playerJoysticks to -1!
        _playersJoystick = new int[4];
        for (int i = 0; i < _playersJoystick.Length; i++)
        {
            _playersJoystick[i] = -1;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        JoystickCheck();
    }

    bool CheckFirstPlayer()
    {
        return (_playersJoystick[0] == -1) ? true : false;
    }

    void JoystickCheck()
    {
        for (int i = 0; i < 16; i++)
        {
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
            if (Input.GetKeyDown("joystick " + (i + 1) + " button 1"))
            {
                print(i + 1);
                for (int p = 0; p < 4; p++)
                {
                    if (_playersJoystick[p] == -1)
                    {
                        _playersJoystick[p] = i + 1;
                        if (CheckFirstPlayer() == true)
                        {
                            _startButton = "joystick " + _playersJoystick[p] + " button 9";
                        }
                        break;
                    }
                }
            }
        }
    }

    public bool CheckPlayer(int p)
    {
        if (_playersJoystick[p] == -1)
        {
            return false;
        }
        return true;
    }

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

    public int PlayerJoystick(int p)
    {
        return _playersJoystick[p];
    }
    //IEnumerator EndGameCheck()
    //{
    //    if (_running)
    //    {
    //        _playerCount = PlayerCount();
    //        if (_playerCount == 1)
    //        {
    //            _audioSource.clip = WinningClip;
    //            _audioSource.Play();
    //            _running = false;
    //            yield return new WaitForSeconds(_audioSource.clip.length);
    //            Application.LoadLevel(1);
    //        }
    //    }
    //}


}
