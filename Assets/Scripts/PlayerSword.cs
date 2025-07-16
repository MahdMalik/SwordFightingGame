using Unity.VisualScripting;
using UnityEngine;

public class PlayerSword : Sword
{
    public Camera cam;
    private bool stabbing;

    public void UpdateSword()
    {
        // Get mouse position and compute goal rotation for parent
        Vector2 pointDirection = cam.ScreenToWorldPoint(Input.mousePosition);
        pointDirection -= swordControllerBody.position;
        UpdateSword(pointDirection, stabbing);

        // Adjust local stabbing offset on Y axis. First, see if pressed
        if (Input.GetMouseButton(0))
        {
            //if it is, being held, extend to where it should go
            if (localOffset.y < STARTING_OFFSET + 0.6)
            {
                stabbing = true;
                localOffset.y += 0.01f;
                swordForce = 300;
            }
        }
        else
        {
            //otherwise, once button is let go, start retracting
            if (localOffset.y > STARTING_OFFSET)
            {
                localOffset.y -= 0.01f;
                swordForce = 0;
            }
        }

        //this way we can end the stabbing when it's retracted enough
        if (Mathf.Approximately(localOffset.y, STARTING_OFFSET))
            stabbing = false;
    }

    
}