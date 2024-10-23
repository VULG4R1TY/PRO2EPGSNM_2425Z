using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    Rigidbody rb;
    SpriteRenderer sr;
    Animator anim;

    public float upForce = 200;
    public float speed = 5;
    public float runSpeed = 10;

    public bool isGrounded = false;

    bool isLeftShift;
    float moveHorizontal;
    float moveVertical;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isLeftShift = Input.GetKey(KeyCode.LeftShift);
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal > 0)
        {
            sr.flipX = false;
        }
        else if (moveHorizontal < 0)
        {
            sr.flipX = true;
        }

        if (moveHorizontal == 0 && moveVertical == 0)
        {
            anim.SetBool("IsRunning", false);
        }
        else
        {
            anim.SetBool("IsRunning", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * upForce);
            isGrounded = false;
            anim.SetBool("isGrounded", false);
            anim.SetTrigger("Jump");
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
    }
}