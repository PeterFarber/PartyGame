using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Thwomper_Minigame : MonoBehaviour
{

    public GameObject ThwomperPrefab;
    public GameObject[] Players;

    public GameObject SoundPlayer;

    public AudioClip WinningClip;
    public AudioClip RumblingClip;
    public AudioClip DropClip;


    private GameObject[] _thwompers;
    private Vector3 _thwomperOffset;

    private AudioSource _audioSource;

    public float _waveTimer;
    public bool _nextWave;
    public float _shakeTimer;
    public bool _shaking;
    private float _dropTimer;
    private bool _dropping;
    public bool _atStart;
    private bool _resetting;

    private int[] _thwompersSelected;
    private bool _running;
    private int _playerCount;

    private int _thwomperDirection;
    private float _thwomperAngle;

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
        _thwompers = new GameObject[7];
        _thwomperOffset = new Vector3(-3, 4, 0);
        _thwompersSelected = null;
        _thwomperDirection = -1;
        _thwomperAngle = 0;

        _waveTimer = 2f;
        _nextWave = false;
        _shakeTimer = 2f;
        _shaking = false;
        _dropping = false;
        _dropTimer = 1.0f;

        _running = false;
        _playerCount = 0;
        _atStart = true;

        _audioSource = GetComponent<AudioSource>();

        //Create Thwompers
        CreateThwompers();

        //Set game is running equal to true!
        _running = true;
        _nextWave = true;

        //Get Player Count.
        _playerCount = PlayerCount();

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
                SceneManager.LoadScene(1);
            }
        }
    }

    void Update()
    {

        StartCoroutine(EndGameCheck());

        if (_running)
        {
            if (_nextWave)
            {
                //Update the countDown timer.
                _waveTimer -= Time.deltaTime;

                //If waveTimer is finished.
                if (_waveTimer <= 0.0f)
                {

                    //Don't Run The Next Wave!
                    _nextWave = false;

                    //Select a random amount of seconds for next waveCounter.
                    _waveTimer = Random.Range(1.0f, 6.0f);

                    //Select Thwompers!
                    SelectThwompers();

                    //Start Shake!
                    _shaking = true;

                    GameObject soundPlayer = Instantiate(SoundPlayer, transform.position, Random.rotation);
                    soundPlayer.GetComponent<SoundPlayer>().Play(RumblingClip);

                }
            }

            if (_shaking)
            {
 
                //Update the countDown timer.
                _shakeTimer -= Time.deltaTime;

                bool dir = false;
                for (int i = 0; i < _thwompersSelected.Length; i++)
                {
                    Rigidbody thwomper = _thwompers[_thwompersSelected[i]].GetComponent<Rigidbody>();
                    _thwomperAngle += Time.deltaTime * _thwomperDirection * 20;
                    thwomper.transform.eulerAngles = new Vector3(0.0f, 0.0f, _thwomperAngle);
                    print(_thwomperAngle);
                    if((_thwomperAngle <= -10 && _thwomperDirection == -1) || (_thwomperAngle >= 10 && _thwomperDirection == 1))
                    {
                        dir = true;
                    }
                    //_thwompers[_thwompersSelected[i]].GetComponent<Renderer>().material.SetColor("_Color", Color.red);

                }
                if (dir)
                {
                    _thwomperDirection *= -1;
                }

                //If waveTimer is finished.
                if (_shakeTimer <= 0.0f)
                {

                    //Don't Run The Next Wave!
                    _shaking = false;

                    //Select a random amount of seconds for next waveCounter.
                    _shakeTimer = Random.Range(1.0f, 4.0f);

                    //Drop!
                    DropThwompers();

                    _dropping = true;

                    GameObject soundPlayer = Instantiate(SoundPlayer, transform.position, Random.rotation);
                    soundPlayer.GetComponent<SoundPlayer>().Play(DropClip);

                    for (int i = 0; i < _thwompersSelected.Length; i++)
                    {
                        Rigidbody thwomper = _thwompers[_thwompersSelected[i]].GetComponent<Rigidbody>();
                        thwomper.transform.eulerAngles = Vector3.zero;
                    }
                }
            }

            if (_dropping)
            {

                //Update the countDown timer.
                _dropTimer -= Time.deltaTime;

                for (int i = 0; i < _thwompersSelected.Length; i++)
                {
                    Rigidbody thwomper = _thwompers[_thwompersSelected[i]].GetComponent<Rigidbody>();
                    if(thwomper.velocity.y <= 7.5f)
                    {
                        thwomper.AddForce(Vector3.down * 1.0f * 100, ForceMode.Impulse);
                    }
                }

                //If waveTimer is finished.
                if (_dropTimer <= 0.0f)
                {

                    //Don't Run The Next Wave!
                    _dropping = false;

                    //Select a random amount of seconds for next waveCounter.
                    _dropTimer = 1.0f;

                    _resetting = true;

                }
            }

            if (_resetting)
            {
                bool done = true;
                for (int i = 0; i < _thwompersSelected.Length; i++)
                {
                    Rigidbody thwomper = _thwompers[_thwompersSelected[i]].GetComponent<Rigidbody>();
                    if (thwomper.isKinematic == false)
                    {
                        thwomper.useGravity = false;
                        done = false;
                        thwomper.position += Vector3.up * Time.deltaTime * 4;
                        if (thwomper.position.y >= _thwomperOffset.y)
                        {
                            _thwompers[_thwompersSelected[i]].gameObject.transform.position = new Vector3(thwomper.position.x, _thwomperOffset.y, thwomper.position.z);
                            print(thwomper.position);
                            thwomper.isKinematic = true;
                        }
                    }
                }
                if(done == true)
                {
                    for (int i = 0; i < _thwompersSelected.Length; i++)
                    {
                        _thwompers[_thwompersSelected[i]].GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    }
                    _resetting = false;
                    _nextWave = true;
                }
            }

        }

    }

    void DropThwompers()
    {
        for (int i = 0; i < _thwompersSelected.Length; i++)
        {
            _thwompers[_thwompersSelected[i]].GetComponent<Rigidbody>().isKinematic = false;
            _thwompers[_thwompersSelected[i]].GetComponent<Rigidbody>().AddForce(Vector3.down * 7.5f * 100, ForceMode.Impulse);
        }
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

    void SelectThwompers()
    {
        _thwompersSelected = null;

        int amount = (int)Random.Range(1.0f, 7.0f);

        _thwompersSelected = new int[amount];

        for (int i = 0; i < amount; i++)
        {
            bool duplicate = true;
            while (duplicate)
            {

                _thwompersSelected[i] = Random.Range(0, 7);
                duplicate = false;
                for (int j = 0; j < amount; j++)
                {
                    if (_thwompersSelected[j] == _thwompersSelected[i] && j != i)
                    {
                        duplicate = true;
                        break;
                    }
                }

            }
        }

    }

    void CreateThwompers()
    {

        for (int i = 0; i < _thwompers.Length; i++)
        {
            _thwompers[i] = Instantiate(ThwomperPrefab, _thwomperOffset + new Vector3(i, 0.0f, 0.0f), transform.rotation);
        }

    }
}
