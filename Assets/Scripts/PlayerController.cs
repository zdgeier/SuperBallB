using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
    public Rigidbody rb;
    public float speed;
    public float vThrust;

    [SyncVar]
    public char currentInput = ' ';

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }

        // DO NOT SEND CMDS ON EVERY FRAME IDIOT
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdJump();
        }

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            CmdMove(moveHorizontal, moveVertical);
        }        
    }

    [Command]
    void CmdMove(float moveHorizontal, float moveVertical)
    {
        Vector3 movement = new Vector3(moveVertical, 0.0f, -moveHorizontal);

        rb.AddTorque(movement * speed);
    }

    [Command]
    void CmdJump()
    {
        rb.AddForce(new Vector3(0, 1, 0) * vThrust);
    }
}
