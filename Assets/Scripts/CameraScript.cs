using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Player thePlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        //orthographic size gets you half of the camera, then we add it to the player's position to make the camera slightly above
        transform.position = new Vector3(thePlayer.transform.position.x, thePlayer.transform.position.y + Camera.main.orthographicSize / 2, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(thePlayer.transform.position.x, thePlayer.transform.position.y + Camera.main.orthographicSize / 2, transform.position.z);
    }
}
