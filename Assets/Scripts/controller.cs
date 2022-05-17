using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    public GameObject camera;

    public const float WALK_SPEED = 2.5f;
    public const float RUN_SPEED = 6f;

    public GameObject leftController, rightController;

    // Update is called once per frame
    void Update()
    {
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

    }
}