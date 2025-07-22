using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Person : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected Sword sword;

    // Start is called before the first frame update
    protected Rigidbody2D personPos;
    protected Animator animator;
    protected bool hitBySword;
    public void AddColliders()
    {
        //want to make sure the player doesn't collide with either the blade or the hilt
        BoxCollider2D blade = transform.Find("SwordContainer").GetChild(0).GetChild(0).Find("Blade").GetComponent<BoxCollider2D>();;
        BoxCollider2D hilt = transform.Find("SwordContainer").GetChild(0).GetChild(0).Find("Hilt").GetComponent<BoxCollider2D>();;
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
                            Physics2D.IgnoreCollision(blade, collideObj, true);
                            Physics2D.IgnoreCollision(hilt, collideObj, true);
                        }
                        else
                        {
                            CircleCollider2D collideObj = grandchild.gameObject.AddComponent<CircleCollider2D>();
                            Physics2D.IgnoreCollision(blade, collideObj, true);
                            Physics2D.IgnoreCollision(hilt, collideObj, true);
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
                    Physics2D.IgnoreCollision(blade, collideObj, true);
                    Physics2D.IgnoreCollision(hilt, collideObj, true);
                }
                else
                {
                    BoxCollider2D collideObj = child.gameObject.AddComponent<BoxCollider2D>();
                    Physics2D.IgnoreCollision(blade, collideObj, true);
                    Physics2D.IgnoreCollision(hilt, collideObj, true);
                }
            }
        }
    }

    protected void Start()
    {
        personPos = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        hitBySword = false;
        AddColliders();
    }

    // Update is called once per frame
    protected void UpdateFrame(float currentSpeed)
    {
        if (hitBySword)
        {
            Destroy(gameObject);
        }
        personPos.position = new Vector2(personPos.position.x + currentSpeed * Time.deltaTime, personPos.position.y);
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
    }

    public void SetHit()
    {
        hitBySword = true;
    }
}
