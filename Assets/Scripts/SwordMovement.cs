using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovement : MonoBehaviour
{
    public Rigidbody2D swordControllerBody;
    public Rigidbody2D playerBody;
    public Rigidbody2D swordBody;
    public Camera cam;
    Vector2 mousePos;
    public float swingSpeed;
    public float currentSwingSpeed;
    public bool stabbing;
    public float swordForce;

    void Update()
    {
        transform.position = new Vector2 (playerBody.position.x, playerBody.position.y);

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - swordControllerBody.position;
        float goalAngle = Mathf.Round(Mathf.Atan2(lookDir.x, -lookDir.y) * Mathf.Rad2Deg) + 180f;
        if (swordControllerBody.rotation > 360f) {
            swordControllerBody.rotation = swordControllerBody.rotation - 360f;
        } else if (swordControllerBody.rotation < 0f) {
            swordControllerBody.rotation = swordControllerBody.rotation + 360f;
        }

        if(stabbing == false) {
        if(goalAngle == Mathf.Round(swordControllerBody.rotation) || goalAngle == Mathf.Round(swordControllerBody.rotation + 360f) || goalAngle == Mathf.Round(swordControllerBody.rotation + 180f)) {
            swordForce = 0f;
        } else if (goalAngle > swordControllerBody.rotation && goalAngle < swordControllerBody.rotation + 180f || goalAngle < swordControllerBody.rotation - 180f && goalAngle > swordControllerBody.rotation - 360f) {
            swordControllerBody.rotation = swordControllerBody.rotation + currentSwingSpeed;
            swordForce++;
            Debug.Log(swordForce);
        } else {
            swordControllerBody.rotation = swordControllerBody.rotation - currentSwingSpeed;
            swordForce++;
        }
        }
        //Debug.Log(swordForce);

        currentSwingSpeed = swingSpeed * (1 + (Mathf.Round(swordForce/100) / 4));
        if(Input.GetMouseButton(0)) {
            if (swordBody.position.y < 1.6) {
            swordBody.position = new Vector2 (swordBody.position.x, swordBody.position.y + 0.01f);
            }
        } else {
            if (swordBody.position.y > 1.0) {
            swordBody.position = new Vector2 (swordBody.position.x, swordBody.position.y - 0.01f);
            }
        }
        if (Mathf.Round(swordBody.position.y * 100) == 100.0) {
            stabbing = false;
        }
        

    }
}
