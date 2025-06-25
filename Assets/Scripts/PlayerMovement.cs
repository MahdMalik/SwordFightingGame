using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Animator animator;
    public float currentSpeed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = Input.GetAxisRaw("Horizontal") * speed;
        transform.position = new Vector2(transform.position.x + currentSpeed * Time.deltaTime, transform.position.y);
        //this way if speed doesn't change, just keep it as is
        if (currentSpeed > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (currentSpeed < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        animator.SetFloat("speed", Math.Abs(currentSpeed));
    }
}
