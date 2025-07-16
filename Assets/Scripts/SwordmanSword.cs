using UnityEngine;

public class SwordmanSword : Sword
{
    public Rigidbody2D playerBody;

    // Update is called once per frame
    public void UpdateSword()
    {
        if (playerBody != null)
        {
            //now can use this to get the angle, by getting change in vector such that it looks like the vector begins at the origin.
            Vector2 pointDirection = playerBody.position - personPos.position;
            UpdateSword(pointDirection, false);
        }

    }
}
