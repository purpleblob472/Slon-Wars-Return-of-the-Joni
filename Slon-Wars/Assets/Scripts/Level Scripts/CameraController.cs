using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Camera pos meant to be accessed by a room script to indicate where the player has moved to
    [HideInInspector]
    public Vector3 cameraPos;

    private float transitionSpeed = 10f;
    void Start()
    {
        cameraPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //If the cameras position is outdate smoothly move it
        //NOTE: Z axiz is affected, make sure cameraPos Z < 0
        if (cameraPos != transform.position)
        {
            transform.position = Vector3.Lerp(transform.position, cameraPos, Time.deltaTime * transitionSpeed);
        }
    }
}