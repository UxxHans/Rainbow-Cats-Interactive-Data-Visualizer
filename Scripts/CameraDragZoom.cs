using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDragZoom : MonoBehaviour
{
    //Set the speed of the control.
    public float scrollSpeed = 2;
    public float lookSpeed = 0.002f;

    Vector3 startMousePosition = Vector3.zero;
    Vector3 startCameraPosition = Vector3.zero;

    public void Update()
    {
        //Get mouse position data on click. (Right - 0 | Left - 1)
        if (Input.GetMouseButtonDown(1))
        {
            startMousePosition = Input.mousePosition;
            startCameraPosition = Camera.main.transform.position;
        }

        //Get mouse drag position and calculate the delta.
        else if (Input.GetMouseButton(1))
        {
            Vector3 mouseDelta = startMousePosition - Input.mousePosition;
            Camera.main.transform.position = startCameraPosition + mouseDelta * Camera.main.fieldOfView * lookSpeed;
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            //Get scroll data.
            float scrollDelta = Input.mouseScrollDelta.y * scrollSpeed;

            //Set camera zoom on different camera types.
            if (Camera.main.orthographic) Camera.main.orthographicSize = Camera.main.orthographicSize - scrollDelta > 0 ? Camera.main.orthographicSize - scrollDelta : Camera.main.orthographicSize;
            else Camera.main.fieldOfView = Camera.main.fieldOfView - scrollDelta > 0 ? Camera.main.fieldOfView - scrollDelta : Camera.main.fieldOfView;
        }
    }
}
