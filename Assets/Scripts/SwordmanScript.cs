using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SwordmanScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D playerPos;
    public SwordmanSwordMovement theSword;
    public float speed;
    private Animator animator;
    private Random rng;
    private bool hitBySword = false;
    private Rigidbody2D swordmanPos;
    void Start()
    {
        animator = GetComponent<Animator>();
        rng = new Random();
        HelperFunctions.AddColliders(transform);
        swordmanPos = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //just make their movement random for now
        float currentSpeed = 0;
        if (playerPos != null)
        {
            if (playerPos.position.x > swordmanPos.position.x)
            {
                //the random is gonna be between 0.5 and 1 through this. first it's between 0 and 1, then divide to get between 0 and .5, then add to get between .5 and 1.
                currentSpeed = (float)(rng.NextDouble() / 2 + 0.5) * speed;
            }
            else if (playerPos.position.x < swordmanPos.position.x)
            {
                currentSpeed = (float)(rng.NextDouble() / 2 + 0.5) * speed * -1;
            }
            swordmanPos.position = new Vector2(swordmanPos.position.x + currentSpeed * Time.deltaTime, swordmanPos.position.y);
            //this way if speed doesn't change, just keep it as is
            if (currentSpeed > 0)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            else if (currentSpeed < 0)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }

            theSword.UpdateSword();

            if (hitBySword)
            {
                Destroy(gameObject);
            }
        }
            animator.SetFloat("speed", Math.Abs(currentSpeed));
    }

    public void SetHit()
    {
        hitBySword = true;
    }
}
