using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    private float movementSpeed = 1.0f;

    private float jumpForce = 3.0f;

    public AudioClip jump;

    public bool isGrounded;
    Rigidbody rb;
    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            audioSource.PlayOneShot(jump);
        }
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

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

}
