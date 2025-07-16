using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Person
{
    // Update is called once per frame
    void Update()
    {
        float theSpeed = Input.GetAxisRaw("Horizontal") * speed;
        UpdateFrame(theSpeed);
    }
}
