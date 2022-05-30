using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controller : MonoBehaviour
{
    public GameObject camera;
    public GameObject leftController, rightController;

    // public const float WALK_SPEED = 2.5f;
    // public const float RUN_SPEED = 6f;
    public const float ROTATE_SPEED = 20f;

    private float befRight, befLeft;    // 양쪽 컨트롤러의 위치
    private bool moveForward, moveRotate;   // 팔을 움직이고 있는지

    private void Start()
    {
        befRight = rightController.transform.localPosition.x;
        befLeft = leftController.transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        float moveRight = rightController.transform.localPosition.x - befRight;
        float moveLeft = befLeft - leftController.transform.localPosition.x;
        Vector3 forward = camera.transform.forward;

        // 양팔을 움직이면 앞으로 전진
        if (moveRight > 0.01f && moveLeft > 0.01f)
        {
            StopCoroutine("MoveForwardCoroutine");
            gameObject.transform.position -= forward * (moveRight + moveLeft);
            moveForward = true;
        }
        // 오른팔만 움직이면 왼쪽으로 회전
        else if (moveRight > 0.01f)
        {
            StopCoroutine("MoveForwardCoroutine");
            StopCoroutine("MoveRotateCoroutine");
            gameObject.transform.position -= forward * moveRight;
            gameObject.transform.rotation *= Quaternion.Euler(new Vector4(0, -ROTATE_SPEED, 0, 0) * moveRight);
            moveRotate = true;
        }
        // 왼팔만 움직이면 오른쪽으로 회전
        else if (moveLeft > 0.01f)
        {
            StopCoroutine("MoveForwardCoroutine");
            StopCoroutine("MoveRotateCoroutine");
            gameObject.transform.position -= forward * moveLeft;
            gameObject.transform.rotation *= Quaternion.Euler(new Vector4(0, ROTATE_SPEED, 0, 0) * moveLeft);
            moveRotate = true;
        }
        // 천천히 전진
        else if (moveForward)
        {
            moveForward = false;
            StartCoroutine("MoveForwardCoroutine", moveRight + moveLeft);
        }
        // 천천히 회전
        else if (moveRotate)
        {
            moveRotate = false;
            StartCoroutine("MoveForwardCoroutine", moveRight > moveLeft ? moveRight : moveLeft);
            StartCoroutine("MoveRotateCoroutine", 
                moveRight > moveLeft ? 
                Quaternion.Euler(new Vector4(0, -ROTATE_SPEED, 0, 0) * moveRight) 
                : Quaternion.Euler(new Vector4(0, ROTATE_SPEED, 0, 0) * moveLeft));
        }

        befRight = rightController.transform.localPosition.x;
        befLeft = leftController.transform.localPosition.x;
    }

    IEnumerator MoveForwardCoroutine(float force)
    {
        Vector3 forward = camera.transform.forward;
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

        while (force > 0.0001f)
        {
            gameObject.transform.position -= forward * force;
            force = Mathf.Lerp(force, 0, Time.deltaTime);

            yield return waitForEndOfFrame;
        }
        StopCoroutine("MoveRotateCoroutine");
    }

    // MoveForwardCoroutine이 종료되면 종료
    IEnumerator MoveRotateCoroutine(Quaternion force)
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

        while (true)
        {
            gameObject.transform.rotation *= force;
            force = Quaternion.Lerp(force, Quaternion.Euler(0, 0, 0), Time.deltaTime);

            yield return waitForEndOfFrame;
        }
    }
}