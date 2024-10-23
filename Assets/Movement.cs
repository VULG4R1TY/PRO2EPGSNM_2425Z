using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    Rigidbody rb;
    SpriteRenderer sr;
    Animator anim;

    public float upForce = 100;
    public float speed = 5;
    public float runSpeed = 2500;

    public bool isGrounded = false;

    bool isLeftShift;
    float moveHorizontal;
    float moveVertical;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        isLeftShift = Input.GetKey(KeyCode.LeftShift);
        //Input.GetAxis("Vertical");
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal > 0)
        {
            sr.flipX = false;
        }
        else if(moveHorizontal < 0)
        {
            sr.flipX = true;
        }

        if (moveHorizontal == 0 && moveVertical == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * upForce);
            isGrounded = false;
            anim.SetBool("isGrounded", false);
            anim.SetBool("Jump", true);
        }

    }

    private void FixedUpdate()
    {
        if (isLeftShift)
        {
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            transform.position += moveDirection * runSpeed * Time.deltaTime;
        }
        else
        {
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            transform.position += moveDirection * speed * Time.deltaTime;
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        anim.SetBool("isGrounded", true);
        anim.SetBool("Jump", false);
    }
}