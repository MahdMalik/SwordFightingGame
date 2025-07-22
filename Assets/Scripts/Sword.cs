using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sword : MonoBehaviour
{
    [SerializeField]
    protected Person person;
    [SerializeField]
    protected float swingSpeed;
    protected Rigidbody2D personPos;
    protected float swordForce;
    protected sbyte previousReflection;
    protected const float STARTING_OFFSET = 2f;
    protected Rigidbody2D swordControllerBody; // Parent Rigidbody2D

    // Local offset of swordBody relative to swordControllerBody, for stabbing animation
    protected Vector2 localOffset = new Vector2(0f, STARTING_OFFSET);

    protected float currentSwingSpeed;
    protected Rigidbody2D swordBody;           // Child Rigidbody2D with its own physics

    // Start is called before the first frame update
    void Start()
    {
        personPos = person.gameObject.GetComponent<Rigidbody2D>();
        previousReflection = (sbyte)person.transform.localScale.x;
        swordControllerBody = GetComponent<Rigidbody2D>();
        swordBody = transform.GetChild(0).GetComponent<Rigidbody2D>();
    }

    public abstract void UpdateSword();

    // Update is called once per frame
    protected virtual void UpdateSword(Vector2 pointDirection, bool stabbing)
    {
        // Move swordControllerBody to current position
        swordControllerBody.position = personPos.position;
        // Normalize rotation angle
        float currentRotation = swordControllerBody.rotation;

        //get the angle through quick arctangent, then get it in degrees. Note that it assumes the axis starts counter-clockwise to your left. To aline it to the top, subtract by 90.
        float goalAngle = Mathf.Atan2(pointDirection.y, pointDirection.x) * Mathf.Rad2Deg - 90;
        if (goalAngle < 0)
        {
            goalAngle += 360;
        }
        // Debug.Log($"Goal angle: {goalAngle}, Current angle: {swordControllerBody.rotation}");

        //if it's negative, we want everything to be from 0 to 360, so do it that way
        if (currentRotation < 0f) currentRotation += 360f;
        //also, we want to make sure it doesn't go past 360
        if (currentRotation >= 360f)
            currentRotation %= 360f;

        //if there's been a reflection, change the rotation acordingly. Change the reflection of the container though instead.
        if (person.transform.localScale.x != previousReflection)
        {
            transform.parent.transform.localScale = new Vector3(transform.parent.transform.localScale.x * -1, transform.parent.transform.localScale.y, transform.parent.transform.localScale.z);
            // Debug.Log("We swapped!");

            //now though, we probably want the blade to face the opposite way. So, we shoudl do just that
            GameObject bladeAndHilt = transform.GetChild(0).gameObject;
            bladeAndHilt.transform.localScale = new Vector3(bladeAndHilt.transform.localScale.x * -1, bladeAndHilt.transform.localScale.y, bladeAndHilt.transform.localScale.z);
        }

        if (!stabbing)
        {
            float travelDistance = Mathf.DeltaAngle(currentRotation, goalAngle);
            if (Mathf.Abs(travelDistance) < currentSwingSpeed)
            {
                swordForce = 0;
                currentRotation = goalAngle;
            }
            else if (travelDistance < 0)
            {
                currentRotation -= currentSwingSpeed;
                swordForce++;
            }
            else
            {
                currentRotation += currentSwingSpeed;
                swordForce++;
            }
            currentSwingSpeed = swingSpeed * (1 + (swordForce / 400));
        }

        // Calculate swordBody world position by rotating local offset by parent's rotation
        //note that rotatedOffset is going to be in terms of x and y coordinates, how much to change it depending on the rotation
        Vector2 rotatedOffset = Quaternion.Euler(0, 0, currentRotation) * localOffset;
        Vector2 desiredPos = swordControllerBody.position + rotatedOffset;

        // Debug.Log("New Current Angle: " + currentRotation);

        previousReflection = (sbyte)person.transform.localScale.x;

        // Move swordBody Rigidbody2D to desired position and rotation matching parent
        swordBody.position = desiredPos;
        swordBody.rotation = currentRotation;
        swordControllerBody.rotation = currentRotation;
    }
}
