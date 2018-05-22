using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpRoper_Minigame : MonoBehaviour
{

    public GameObject[] Players;

    public GameObject SoundPlayer;

    public AudioClip WinningClip;


    private AudioSource _audioSource;

    public  GameObject _jumpRope;

    public GameObject[] _platforms;

    public float _waveTimer;
    public bool _nextWave;
    public float _shakeTimer;
    public bool _shaking;
    private float _startTimer;
    public bool _atStart;

    private bool _running;
    private int _playerCount;

    private float _rotateSpeed;
    private bool _speedingUp;
    private bool _transition;

    private Joystick_Controller _joystickController;

    private Score_Controller _scoreController;

    void Start()
    {

        _joystickController = GameObject.Find("Global_Controller").GetComponent<Joystick_Controller>();

        for (int i = 0; i < 4; i++)
        {
            if (_joystickController.CheckPlayer(i))
            {
                Players[i].GetComponent<Player_Controller>().setJoystick(_joystickController.PlayerJoystick(i));
                Players[i].SetActive(true);
            }
        }

        _scoreController = GameObject.Find("Global_Controller").GetComponent<Score_Controller>();

        //Initialize Varaibles!
        _rotateSpeed = 4f;
        _startTimer = 0f;
        _waveTimer = 2f;
        _running = false;
        _playerCount = 0;
        _atStart = true;
        _speedingUp = true;
        _shakeTimer = Random.Range(1.0f, 100.0f);
        _audioSource = GetComponent<AudioSource>();

        _running = true;
        _playerCount = PlayerCount();

        for (int i = 0; i < _playerCount; i++)
        {
            _platforms[i].SetActive(true);
        }
    }

    IEnumerator EndGameCheck()
    {
        if (_running)
        {
            _playerCount = PlayerCount();
            if (_playerCount == 1)
            {
                for (int i = 0; i < Players.Length; i++)
                {
                    if (Players[i].activeInHierarchy)
                    {
                        _scoreController.IncrementScore(i);
                    }
                }
                _audioSource.clip = WinningClip;
                _audioSource.Play();
                _running = false;
                yield return new WaitForSeconds(_audioSource.clip.length);
                SceneManager.LoadScene("HexagonHeat");
            }
        }
    }

    void Update()
    {
        if (_speedingUp == true)
        {
            _rotateSpeed += 0.01f;
        }
        else
        {
            _rotateSpeed -= 0.01f;
        }
        if(_rotateSpeed > 8)
        {

            _speedingUp = false;
        }
        if(_rotateSpeed < 2){
            _speedingUp = true;
        }
        if (_startTimer > 10)
        {
            _jumpRope.transform.Rotate(_rotateSpeed, 0, 0);
        }
        else
        {
            _startTimer += 0.1f;
        }
        StartCoroutine(EndGameCheck());
        RenderSettings.skybox.SetFloat("_Rotation", Time.time*5);

    }

    int PlayerCount()
    {
        int sum = 0;
        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i].activeInHierarchy == true)
            {
                sum++;
            }
        }
        return sum;
    }

}
