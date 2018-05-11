using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThwomper : MonoBehaviour {

    private float movementSpeed = 1.0f;

    private float jumpForce = 3.0f;

    public AudioClip jump;
    public AudioClip bounce;

    public bool isGrounded;
    Rigidbody rb;
    AudioSource audioSource;


    public string jump_input;
    public string h_input;
    public string v_input;
    public Color color;

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        this.GetComponentInChildren<Renderer>().material.SetColor("_Color", color);
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown(jump_input) && this.GetComponent<CharacterController>().isGrounded == true)
        //{
        //    print("hello");
        //    audioSource.PlayOneShot(jump);
        //    isGrounded = false;
        //    float cY = rb.velocity.y;
        //    float nY = Mathf.Abs(cY - jumpForce);
        //    rb.AddForce(Vector3.up * nY, ForceMode.Impulse);
        //}

        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis(h_input), 0, 0);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton(jump_input)) {
                audioSource.PlayOneShot(jump);
                moveDirection.y = jumpSpeed;
            }

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        //float moveHorizontal = Input.GetAxisRaw(h_input);
        //float moveVertical = Input.GetAxisRaw(v_input);

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //if (moveHorizontal != 0.0f || moveVertical != 0.0f)
        //{
        //    transform.rotation = Quaternion.LookRotation(movement);
        //    transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
        //}
    }


    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void OnCollisionEnter(Collision c)
    {
        // force is how forcefully we will push the player away from the enemy.
        float force = 100;

        // If the object we hit is the enemy
        if (c.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(bounce);

            // Calculate Angle Between the collision point and the player
            Vector3 dir = c.gameObject.transform.position - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            GetComponent<Rigidbody>().AddForce(dir * force);
        }
        else if (c.gameObject.tag == "Lava")
        {
            this.gameObject.SetActive(false);
        }
    }

}
