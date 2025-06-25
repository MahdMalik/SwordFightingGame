using UnityEngine;

public class SwordMovement : MonoBehaviour
{
    public Rigidbody2D swordControllerBody; // Parent Rigidbody2D
    public Rigidbody2D playerBody;          // Player Rigidbody2D
    public Rigidbody2D swordBody;           // Child Rigidbody2D with its own physics
    public Camera cam;
    public float swingSpeed = 2f;
    public float currentSwingSpeed;
    public bool stabbing;
    public float swordForce;

    // Local offset of swordBody relative to swordControllerBody, for stabbing animation
    private Vector2 localOffset = new Vector2(0f, 1f);

    void Update()
    {
        // Move swordControllerBody to player position
        swordControllerBody.position = playerBody.position;

        // Get mouse position and compute goal rotation for parent
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //this is like the vector where swordControllerBody is the origin, thus letting us easily get the angle
        Vector2 lookDir = mousePos - swordControllerBody.position;

        //get the angle through quick arctangent, then get it in degrees. Note that it assumes the axis starts counter-clockwise to your right. To aline it to the top, subtract by 90.
        float goalAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        // Normalize rotation angle
        float currentRotation = swordControllerBody.rotation;
        //if it's negative, we want everything to be from 0 to 360, so do it that way
        if (currentRotation < 0f) currentRotation += 360f;
        //also, we want to make sure it doesn't go past 360
        if (currentRotation >= 360f)
            currentRotation %= 360f;

        //change it so now it's correct
        swordControllerBody.rotation = currentRotation;

        //if not stabbing, do this
        if (!stabbing)
        {
            //first, get distance left to travel
            float diff = Mathf.DeltaAngle(currentRotation, goalAngle);

            //if we can reach it now in the next frame, get to it
            if (Mathf.Abs(diff) < currentSwingSpeed)
            {
                swordControllerBody.rotation = goalAngle;
                swordForce = 0f;
            }
            //if not, grow closer to its location. If we have to go forwards to get closer, go forwards
            else if (diff > 0)
            {
                swordControllerBody.rotation += currentSwingSpeed;
                swordForce++;
            }
            //if have to go backwards to get closer, go backwards
            else
            {
                swordControllerBody.rotation -= currentSwingSpeed;
                swordForce++;
            }
        }

        //calculate the new swing speed from the force.
        currentSwingSpeed = swingSpeed * (1 + (swordForce / 400));

        // Adjust local stabbing offset on Y axis. First, see if pressed
        if (Input.GetMouseButton(0))
        {
            //if it is, being held, extend to where it should go
            if (localOffset.y < 1.6f)
            {
                stabbing = true;
                localOffset.y += 0.01f;
                swordForce = 300;
            }

        }
        else
        {
            //otherwise, once button is let go, start retracting
            if (localOffset.y > 1.0f)
            {
                localOffset.y -= 0.01f;
                swordForce = 0;
            }
        }

        //this way we can end the stabbing when it's retracted enough
        if (Mathf.Approximately(localOffset.y, 1.0f))
            stabbing = false;
    }

    void FixedUpdate()
    {
        // Calculate swordBody world position by rotating local offset by parent's rotation

        //note that rotatedOffset is going to be in terms of x and y coordinates, how much to change it depending on the rotation
        Vector2 rotatedOffset = Quaternion.Euler(0, 0, swordControllerBody.rotation) * localOffset;
        Vector2 desiredPos = swordControllerBody.position + rotatedOffset;

        // Move swordBody Rigidbody2D to desired position and rotation matching parent
        swordBody.MovePosition(desiredPos);
        swordBody.MoveRotation(swordControllerBody.rotation);
    }
}