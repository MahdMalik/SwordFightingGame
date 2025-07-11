using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperFunctions
{
    public static void AddColliders(Transform transform)
    {
        BoxCollider2D swordObject = transform.Find("SwordContainer").GetChild(0).GetChild(0).gameObject.GetComponent<BoxCollider2D>();;
        foreach (Transform child in transform)
        {
            //for the arm and leg components
            if (child.transform.childCount > 0)
            {
                if (child.transform.name != "SwordContainer")
                {
                    foreach (Transform grandchild in child.transform)
                    {
                        //if it's an upper arm, lower arm, upper leg, or lower leg type, we should make it a rectangle
                        if (grandchild.transform.name.IndexOf("Upper") != -1 || grandchild.transform.name.IndexOf("Lower") != -1)
                        {
                            BoxCollider2D collideObj = grandchild.gameObject.AddComponent<BoxCollider2D>();
                            Physics2D.IgnoreCollision(swordObject, collideObj, true);
                        }
                        else
                        {
                            CircleCollider2D collideObj = grandchild.gameObject.AddComponent<CircleCollider2D>();
                            Physics2D.IgnoreCollision(swordObject, collideObj, true);
                        }
                    }
                }
            }
            //otherwise, can just give it the component as here
            else
            {
                if (child.transform.name == "Head")
                {
                    CircleCollider2D collideObj = child.gameObject.AddComponent<CircleCollider2D>();
                    Physics2D.IgnoreCollision(swordObject, collideObj, true);
                }
                else
                {
                    BoxCollider2D collideObj = child.gameObject.AddComponent<BoxCollider2D>();
                    Physics2D.IgnoreCollision(swordObject, collideObj, true);
                }
            }
        }
    }
}
