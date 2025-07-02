using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollisions : MonoBehaviour
{
    //we here collide with a limb part of the sowrdman, we have to actually get the swordman object
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("A COLLISION HAPPENED!");
        GameObject otherObj = collision.gameObject;
        //we know the Swordman gameobject have a parent, so fi this doesn't, means we haven't found our sword object
        while (otherObj.transform.parent != null)
        {
            if (otherObj.name == "Swordman")
            {
                //now, we have the sowrdman object so we can delete it
                SwordmanScript swordScript = otherObj.GetComponent<SwordmanScript>();
                swordScript.SetHit();
                Debug.Log("Eliminating swordsman!");
                break;
            }
            otherObj = otherObj.transform.parent.gameObject;
        }
    }
}