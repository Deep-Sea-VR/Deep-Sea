using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controller : MonoBehaviour
{
    public GameObject camera;

    public const float WALK_SPEED = 2.5f;
    public const float RUN_SPEED = 6f;

    public GameObject leftController, rightController;

    float befRight, befLeft;

    private void Start()
    {
        befRight = rightController.transform.localPosition.x;
        befLeft = leftController.transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Vector3 movement = camera.transform.TransformDirection(input.x, 0, input.y);
        movement.y = 0;
        movement = movement.magnitude == 0 ? Vector3.zero : movement / movement.magnitude;
        movement *= Time.deltaTime * (OVRInput.Get(OVRInput.Button.PrimaryThumbstick) ? RUN_SPEED : WALK_SPEED) * input.magnitude;
        this.transform.Translate(movement);
        

        //Controller Tracking
        leftController.transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        leftController.transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        rightController.transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        rightController.transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
        */

        float moveRight = rightController.transform.localPosition.x - befRight;
        float moveLeft = befLeft - leftController.transform.localPosition.x;
        Vector3 forward = gameObject.transform.forward;

        if (moveRight > 0)
        {
            gameObject.transform.position -= forward * moveRight;
            gameObject.transform.rotation *= Quaternion.Euler(new Vector4(0, -30, 0, 0) * moveRight);
        }
        if (moveLeft > 0)
        {
            gameObject.transform.position -= forward * moveLeft;
            gameObject.transform.rotation *= Quaternion.Euler(new Vector4(0, 30, 0, 0) * moveLeft);
        }
        befRight = rightController.transform.localPosition.x;
        befLeft = leftController.transform.localPosition.x;
    }
}