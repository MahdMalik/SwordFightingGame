using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class Swordman : Person
{
    // Start is called before the first frame update
    public Rigidbody2D playerPos;
    private Random rng = new Random();

    // Update is called once per frame
    void Update()
    {
        //just make their movement random for now
        float theSpeed = 0;
        if (playerPos != null)
        {
            if (playerPos.position.x > personPos.position.x)
            {
                //the random is gonna be between 0.5 and 1 through this. first it's between 0 and 1, then divide to get between 0 and .5, then add to get between .5 and 1.
                theSpeed = (float)(rng.NextDouble() / 2 + 0.5) * speed;
            }
            else if (playerPos.position.x < personPos.position.x)
            {
                theSpeed = (float)(rng.NextDouble() / 2 + 0.5) * speed * -1;
            }

        }
        UpdateFrame(theSpeed);
    }
}
