using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controller : MonoBehaviour
{
    public GameObject camera;
    public GameObject leftController, rightController;
    public GameObject forwardDirection;

    public const float WALK_SPEED = 10f;
    // public const float RUN_SPEED = 6f;
    public const float ROTATE_SPEED = 20f;

    private float befRight, befLeft;    // ���� ��Ʈ�ѷ��� ��ġ
    private bool isMoveForward;
    // private bool moveRotate;   // ���� �����̰� �ִ���

    private float force;

    private void Start()
    {
        befRight = rightController.transform.localPosition.x;
        befLeft = leftController.transform.localPosition.x;

        StartCoroutine(VibrateController(0.05f, 0.3f, 0.2f, OVRInput.Controller.All));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveRight = rightController.transform.localPosition.x - befRight;
        float moveLeft = befLeft - leftController.transform.localPosition.x;
        Vector3 forward = forwardDirection.transform.forward;

        // ������ �����̸� ������ ����
        if (moveRight > 0.01f && moveLeft > 0.01f)
        {
            //StopCoroutine("MoveForwardCoroutine");
            //Debug.Log("Update "+forward * WALK_SPEED * (moveRight + moveLeft));
            //moveRotate = false;

            gameObject.transform.position += forward * WALK_SPEED * (moveRight + moveLeft);
            isMoveForward = true;
            force = WALK_SPEED * 0.02f;
        }
        // õõ�� ����
        else if (isMoveForward)
        {
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
        // �����ȸ� �����̸� �������� ȸ��
        else if (moveRight > 0.01f)
        {
            //StopCoroutine("MoveForwardCoroutine");
            //StopCoroutine("MoveRotateCoroutine");
            gameObject.transform.position -= forward * moveRight;
            gameObject.transform.rotation *= Quaternion.Euler(new Vector4(0, -ROTATE_SPEED, 0, 0) * moveRight);
            moveForward = false;
            moveRotate = true;
        }
        // ���ȸ� �����̸� ���������� ȸ��
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
        // õõ�� ȸ��
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
        force *= 2; // ����

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

    // MoveForwardCoroutine�� ����Ǹ� ����
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
        OVRInput.SetControllerVibration(0, 0, controller);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bubbles")
        {
            StartCoroutine(VibrateController(0.05f, 0.3f, 0.2f, OVRInput.Controller.All));
        }
    }
}