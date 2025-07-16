using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Person
{    public SwordMovement sword;

    // Update is called once per frame
    void Update()
    {
        float theSpeed = Input.GetAxisRaw("Horizontal") * speed;
        sword.UpdateSword();
        base.UpdateFrame(theSpeed);
    }
}
