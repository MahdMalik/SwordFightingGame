using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SwordmanScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Animator animator;
    private Random rng;
    void Start()
    {
        animator = GetComponent<Animator>();
        rng = new Random();
    }

    // Update is called once per frame
    void Update()
    {
        //just make their movement random for now
        float currentSpeed = (float) (rng.NextDouble() * 2 - 1) * speed;
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
