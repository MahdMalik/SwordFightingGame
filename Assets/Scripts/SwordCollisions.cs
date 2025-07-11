using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordCollisions : MonoBehaviour
{
    //we here collide with a limb part of the sowrdman, we have to actually get the swordman object
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherObj = collision.gameObject;
        Debug.Log("A COLLISION HAPPENED WITH " + otherObj.name);

        //for now if we clash with another sword, want to ignore it
        if (otherObj.name == "Sword")
        {
            return;
        }

        if (otherObj.name == "Swordman")
        {
            //now, we have the sowrdman object so we can delete it
            SwordmanScript swordScript = otherObj.GetComponent<SwordmanScript>();
            swordScript.SetHit();
            Debug.Log("Eliminating swordsman!");
            return;
        }
        else if (otherObj.name == "Player")
        {
            Debug.Log("Eliminating player!");
            //now, we have the sowrdman object so we can delete it
            Player playerScript = otherObj.GetComponent<Player>();
            playerScript.SetHit();
            return;
        }
    }
}