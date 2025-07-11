using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public SwordMovement sword;
    private Animator animator;
    private float currentSpeed = 0f;
    private Rigidbody2D player;
    private bool hitBySword;

    void Start()
    {
        animator = GetComponent<Animator>();
        HelperFunctions.AddColliders(transform);
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = Input.GetAxisRaw("Horizontal") * speed;
        player.position = new Vector2(player.position.x + currentSpeed * Time.deltaTime, player.position.y);
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

        sword.UpdateSword();

        if (hitBySword)
        {
            Destroy(gameObject);
        }
    }
    
    public void SetHit()
    {
        hitBySword = true;
    }
}
