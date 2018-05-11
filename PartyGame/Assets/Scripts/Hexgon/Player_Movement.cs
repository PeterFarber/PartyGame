using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

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

    // Use this for initialization
    void Start()
    {
        this.GetComponentInChildren<Renderer>().material.SetColor("_Color", color);
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(jump_input) && isGrounded)
        {
            audioSource.PlayOneShot(jump);
            isGrounded = false;
            float cY = rb.velocity.y;
            float nY = Mathf.Abs(cY - jumpForce);
            rb.AddForce(Vector3.up * nY, ForceMode.Impulse);
        }
        float moveHorizontal = Input.GetAxisRaw(h_input);
        float moveVertical = Input.GetAxisRaw(v_input);

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (moveHorizontal != 0.0f || moveVertical != 0.0f)
        {
            transform.rotation = Quaternion.LookRotation(movement);
            transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
        }
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
        }else if(c.gameObject.tag == "Lava")
        {
            this.gameObject.SetActive(false);
        }
    }

}
