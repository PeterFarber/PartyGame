using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

    private float movementSpeed = 1.0f;
    private bool canJump = false;
    private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            canJump = true;
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

    void FixedUpdate()
    {
        if (canJump)
        {
            canJump = false;
            rigidbody.AddForce(0, 50, 0, ForceMode.Impulse);
        }
    }

}
