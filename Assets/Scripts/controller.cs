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
        Debug.Log("rightController.transform : " + rightController.transform.localPosition.x + ", " + rightController.transform.localPosition.y + "," + rightController.transform.localPosition.z);
        Debug.Log("gameObject.rotation : " + gameObject.transform.localRotation.x + ", " + gameObject.transform.localRotation.y + ", " + gameObject.transform.localRotation.z);
        Debug.Log("gameObject.transform : " + gameObject.transform.position.x + ", " + gameObject.transform.position.y + ", " + gameObject.transform.position.z);

        float move = (rightController.transform.localPosition.x - befRight);
        if (move > 0)
        {
            Vector3 dir = gameObject.transform.forward;
            gameObject.transform.position -= dir * move;
        }
        befRight = rightController.transform.localPosition.x;
    }
}