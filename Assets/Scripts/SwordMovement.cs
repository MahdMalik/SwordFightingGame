using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovement : MonoBehaviour
{
    public Rigidbody2D swordBody;
    public Rigidbody2D playerBody;
    public Camera cam;
    Vector2 mousePos;
    public float swingSpeed;
    public float currentSwingSpeed;
    public bool stabbing;
    public float swordForce;

    void Update()
    {
        transform.position = playerBody.position;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - swordBody.position;
        float goalAngle = Mathf.Round(Mathf.Atan2(lookDir.x, -lookDir.y) * Mathf.Rad2Deg) + 180f;
        if (swordBody.rotation > 360f) {
            swordBody.rotation = swordBody.rotation - 360f;
        } else if (swordBody.rotation < 0f) {
            swordBody.rotation = swordBody.rotation + 360f;
        }

        if(goalAngle == Mathf.Round(swordBody.rotation) || goalAngle == Mathf.Round(swordBody.rotation + 360f) || goalAngle == Mathf.Round(swordBody.rotation + 180f)) {
            swordForce = 0f;
        } else if (goalAngle > swordBody.rotation && goalAngle < swordBody.rotation + 180f || goalAngle < swordBody.rotation - 180f && goalAngle > swordBody.rotation - 360f) {
            swordBody.rotation = swordBody.rotation + currentSwingSpeed;
            swordForce++;
        } else {
            swordBody.rotation = swordBody.rotation - currentSwingSpeed;
            swordForce++;
        }
        //Debug.Log(swordForce);

        currentSwingSpeed = swingSpeed * (1 + (Mathf.Round(swordForce/100) / 4));


    }
}
