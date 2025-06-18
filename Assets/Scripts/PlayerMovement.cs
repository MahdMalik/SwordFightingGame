using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentSpeed = Input.GetAxisRaw("Horizontal") * speed;
        transform.position = new Vector2(transform.position.x + currentSpeed * Time.deltaTime, transform.position.y);
        if (currentSpeed >= 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.x, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        animator.SetFloat("speed", Math.Abs(currentSpeed));
    }
}
