using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
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
        if(cameraPos != transform.position)
        {
            transform.position = Vector3.Lerp(transform.position, cameraPos, Time.deltaTime * transitionSpeed);
        }
    }
}
