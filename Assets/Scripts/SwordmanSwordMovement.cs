using UnityEngine;

public class SwordmanSwordMovement : MonoBehaviour
{
    public float swingSpeed = 2f;
    public Rigidbody2D swordBody;           // Child Rigidbody2D with its own physics
    public Rigidbody2D playerBody;
    public Swordman swordman;
    private Rigidbody2D swordmanBody;          // Player Rigidbody2D
    private Rigidbody2D swordControllerBody; // Parent Rigidbody2D
    private float currentSwingSpeed;
    private float swordForce;
    private sbyte previousReflection;

    // Local offset of swordBody relative to swordControllerBody, for stabbing animation

    private const float STARTING_OFFSET = 2f;
    private Vector2 localOffset = new Vector2(0f, STARTING_OFFSET);

    // Start is called before the first frame update
    void Start()
    {
        previousReflection = (sbyte)swordman.transform.localScale.x;
        swordmanBody = swordman.GetComponent<Rigidbody2D>();
        swordControllerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void UpdateSword()
    {
        swordControllerBody.position = swordmanBody.position;
        float currentRotation = swordControllerBody.rotation;
        if (playerBody != null)
        {
            //now can use this to get the angle, by getting change in vector such that it looks like the vector begins at the origin.
            Vector2 pointDirection = playerBody.position - swordmanBody.position;

            float goalAngle = (float)Mathf.Atan2(pointDirection.y, pointDirection.x) * Mathf.Rad2Deg - 90f;
            if (goalAngle < 0) goalAngle += 360;

            if (currentRotation < 0) currentRotation += 360;
            if (currentRotation >= 360) currentRotation %= 360;

            //if there's been a reflection, change the rotation acordingly. Change the reflection of the container though instead.
            if (swordman.transform.localScale.x != previousReflection)
            {
                transform.parent.transform.localScale = new Vector3(transform.parent.transform.localScale.x * -1, transform.parent.transform.localScale.y, transform.parent.transform.localScale.z);
                // Debug.Log("We swapped!");
            }

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

        previousReflection = (sbyte)swordman.transform.localScale.x;

        // Move swordBody Rigidbody2D to desired position and rotation matching parent
        swordBody.position = desiredPos;
        swordBody.rotation = currentRotation;
        swordControllerBody.rotation = currentRotation;
    }
}
