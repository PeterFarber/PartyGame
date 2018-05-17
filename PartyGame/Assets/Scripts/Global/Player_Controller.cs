using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    public GameObject particle;
    public GameObject SoundPlayer;

    public float Speed = 1f;
    public float JumpHeight = 1f;
    public float DashDistance = 5f;
    public int JumpCount = 1;
    public int DashCount = 1;
    public float DashTimer;


    private int _jumpCount;
    private int _dashCount;
    private float _dashTimerReset;

    public Color Color;

    [Header("Movement Constraints")]
    public bool MovementX;
    public bool MovementZ;

    [Header("Sound Clips")]
    public AudioClip JumpClip;
    public AudioClip BounceClip;


    private Rigidbody _body;
    private AudioSource _audioSource;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;

    private int _joystick;


    void Awake()
    {

    }


    void Start()
    {
        _dashTimerReset = DashTimer = 1.5f;
        _jumpCount = JumpCount;
        _dashCount = DashCount;
        _body = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        GetComponentInChildren<Renderer>().material.SetColor("_Color", Color);
    }


    void Update()
    {

        if(DashCount < _dashCount)
        {
            DashTimer -= Time.deltaTime;
            if(DashTimer <= 0)
            {
                DashCount++;
                DashTimer = _dashTimerReset;
            }
        }

        _inputs = Vector3.zero;
        if (!MovementX)
        {
            _inputs.x = Input.GetAxis("P" + _joystick + "_Horizontal");
        }
        if (!MovementZ)
        {
            _inputs.z = Input.GetAxis("P" + _joystick + "_Vertical");
        }
        if (_inputs != Vector3.zero)
            transform.forward = _inputs;

        if (Input.GetKeyDown("joystick " + _joystick + " button 1") && JumpCount > 0)
        {
            if (!_isGrounded)
            {
                JumpCount--;
            }
            _audioSource.PlayOneShot(JumpClip);
            float cY = _body.velocity.y;
            float nY = Mathf.Abs(cY - JumpHeight);
            _body.AddForce(Vector3.up *  nY, ForceMode.Impulse);
        }
        if (Input.GetKeyDown("joystick " + _joystick + " button 2") && DashCount >= 1)
        {
            DashCount--;
            Vector3 dashVelocity = Vector3.Scale(transform.forward, DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime)));
            _body.AddForce(dashVelocity, ForceMode.VelocityChange);
        }
    }


    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }


    public void setJoystick(int j)
    {
        _joystick = j;
    }

    void OnCollisionStay(Collision c)
    {
        if (c.gameObject.CompareTag("Ground"))
        {
            JumpCount = _jumpCount;
            _isGrounded = true;
        }
       

    }

    void OnCollisionEnter(Collision c)
    {
        float force = 100;

        if (c.gameObject.tag == "Player")
        {
            _audioSource.PlayOneShot(BounceClip);

            Vector3 dir = c.gameObject.transform.position - transform.position;
            dir = -dir.normalized;

            GetComponent<Rigidbody>().AddForce(dir * force);
        }
        else if (c.gameObject.tag == "Lava" || c.gameObject.tag == "Enemy")
        {
            GameObject soundPlayer = Instantiate(SoundPlayer, transform.position, Random.rotation);
            soundPlayer.GetComponent<SoundPlayer>().Play(BounceClip);

            this.gameObject.SetActive(false);
            for (int h = 0; h < 18; h++)
            {
                GameObject part = Instantiate(particle, transform.position, Random.rotation);
                part.GetComponent<Rigidbody>().AddForce(part.transform.forward * 5, ForceMode.Impulse);
                part.GetComponent<Renderer>().material.SetColor("_Color", Color);
            }
        }
    }

    void OnCollisionExit(Collision c)
    {
        if (c.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
            JumpCount--;
            if(DashCount > 1) {
                DashCount = 1;
            }
        }

    }

}
