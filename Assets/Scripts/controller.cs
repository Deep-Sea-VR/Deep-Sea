using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controller : MonoBehaviour
{
    public GameObject camera;
    public GameObject leftController, rightController;
    public GameObject forwardDirection;

    public const float WALK_SPEED = 13f;
    // public const float RUN_SPEED = 6f;
    public const float ROTATE_SPEED = 20f;

    private float befRight, befLeft;    // 양쪽 컨트롤러의 위치
    private bool isMoveForward;
    // private bool moveRotate;   // 팔을 움직이고 있는지

    private float force;

    public AudioSource swimmingSound;

    public GameObject map;

    private void Start()
    {
        befRight = rightController.transform.localPosition.x;
        befLeft = leftController.transform.localPosition.x;

        swimmingSound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveRight = rightController.transform.localPosition.x - befRight;
        float moveLeft = befLeft - leftController.transform.localPosition.x;
        Vector3 forward = forwardDirection.transform.forward;

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            map.SetActive(!map.activeSelf);
        }

        // 양팔을 움직이면 앞으로 전진
        if (moveRight > 0.01f && moveLeft > 0.01f)
        {
            swimmingSound.Play();
            //StopCoroutine("MoveForwardCoroutine");
            //Debug.Log("Update "+forward * WALK_SPEED * (moveRight + moveLeft));
            //moveRotate = false;

            gameObject.transform.position += forward * WALK_SPEED * (moveRight + moveLeft);
            isMoveForward = true;
            force = WALK_SPEED * 0.02f;
        }
        // 천천히 전진
        else if (isMoveForward)
        {
            swimmingSound.Stop();
            // StopCoroutine("MoveForwardCoroutine");
            // Debug.Log("moveForward");
            //moveForward = false;
            //StartCoroutine("MoveForwardCoroutine", WALK_SPEED * 0.02f);

            gameObject.transform.position += forward * force;
            Debug.Log("MoveForwardCoroutine " + forward * force);
            force = Mathf.Lerp(force, 0, Time.deltaTime);

            if (force < 0.01f)
                isMoveForward = false;
        }

        
        /*
        // 오른팔만 움직이면 왼쪽으로 회전
        else if (moveRight > 0.01f)
        {
            //StopCoroutine("MoveForwardCoroutine");
            //StopCoroutine("MoveRotateCoroutine");
            gameObject.transform.position -= forward * moveRight;
            gameObject.transform.rotation *= Quaternion.Euler(new Vector4(0, -ROTATE_SPEED, 0, 0) * moveRight);
            moveForward = false;
            moveRotate = true;
        }
        // 왼팔만 움직이면 오른쪽으로 회전
        else if (moveLeft > 0.01f)
        {
            //StopCoroutine("MoveForwardCoroutine");
            //StopCoroutine("MoveRotateCoroutine");
            gameObject.transform.position -= forward * moveLeft;
            gameObject.transform.rotation *= Quaternion.Euler(new Vector4(0, ROTATE_SPEED, 0, 0) * moveLeft);
            moveForward = false;
            moveRotate = true;
        }
        */

        /*
        // 천천히 회전
        else if (moveRotate)
        {
            Debug.Log("moveRotate");
            moveRotate = false;
            StartCoroutine("MoveForwardCoroutine", moveRight > moveLeft ? moveRight : moveLeft);
            StartCoroutine("MoveRotateCoroutine", 
                moveRight > moveLeft ? 
                Quaternion.Euler(new Vector4(0, -ROTATE_SPEED, 0, 0) * moveRight) 
                : Quaternion.Euler(new Vector4(0, ROTATE_SPEED, 0, 0) * moveLeft));
        }
        */

        befRight = rightController.transform.localPosition.x;
        befLeft = leftController.transform.localPosition.x;
    }

    /*
    IEnumerator MoveForwardCoroutine(float force)
    {
        //forward = forwardDirection.transform.forward;
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        force *= 2; // 보정

        while (force > 0.001f)
        {
            gameObject.transform.position += forward * force;
            Debug.Log("MoveForwardCoroutine " + forward * force);
            force = Mathf.Lerp(force, 0, Time.deltaTime);

            yield return waitForEndOfFrame;
        }
        yield return null;
        //StopCoroutine("MoveRotateCoroutine");
    }

    // MoveForwardCoroutine이 종료되면 종료
    IEnumerator MoveRotateCoroutine(Quaternion force)
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

        while (true)
        {
            Debug.Log("MoveRotateCoroutine "+force);
            gameObject.transform.rotation *= force;
            force = Quaternion.Lerp(force, Quaternion.Euler(0, 0, 0), Time.deltaTime);

            yield return waitForEndOfFrame;
        }
    }
    */

    protected IEnumerator VibrateController(float waitTime, float frequency, float amplitude, OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, controller);
        yield return new WaitForSeconds(waitTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bubbles")
        {
            StartCoroutine(VibrateController(0.05f, 0.3f, 0.2f, OVRInput.Controller.All));
        } else if (other.name == "Jellyfish")
        {
            StartCoroutine(VibrateController(0.05f, 0.1f, 0.4f, OVRInput.Controller.All));
        }
        else if (other.name == "Fishes")
        {
            StartCoroutine(VibrateController(0.05f, 0.4f, 0.6f, OVRInput.Controller.All));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Bubbles" || other.name == "Jellyfish" || other.name == "Fishes")
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.All);
        }
    }
}