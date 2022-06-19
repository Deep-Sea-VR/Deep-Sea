using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class controller : MonoBehaviour
{
    public GameObject map;
    public GameObject camera;
    public GameObject leftController, rightController;
    public GameObject forwardDirection;

    //public Rigidbody rigid;

    public const float WALK_SPEED = 10f;
    // public const float RUN_SPEED = 6f;
    public const float ROTATE_SPEED = 20f;
    public const float MOVE_DELAY = 3f;

    private float befRight, befLeft;    // ���� ��Ʈ�ѷ��� ��ġ
    private bool isMoveForward;         // ���ġ�� �� �� ���� �ȿ��������� ������ ���ư��� ����
    // private bool moveRotate;
    private float maxSpeed;             // ���ġ�� �� ��Ʈ�ѷ��� �ִ� ������
    private float force;                // isMoveForward ���¿����� �̵� ����
    private bool canMove;               // ���� ���� �������� ���ϵ��� �ϴ� �÷���

    public AudioSource swimmingSound;

    private void Start()
    {
        StartCoroutine("MoveDelayCoroutine");    // ���� ���Ŀ��� �������� ���ϰ�
    }

    private void Update()
    {
        // ���� ����
        if (OVRInput.GetDown(OVRInput.Button.One)) // Button A
            map.SetActive(!map.activeSelf);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            float moveRight = rightController.transform.localPosition.x - befRight;
            float moveLeft = befLeft - leftController.transform.localPosition.x;
            Vector3 forward = forwardDirection.transform.forward;


            // ������ �����̸� ������ ����
            if (moveRight > 0.01f && moveLeft > 0.01f)
            {
                swimmingSound.Play();
                //StopCoroutine("MoveForwardCoroutine");
                //Debug.Log("Update "+forward * WALK_SPEED * (moveRight + moveLeft));
                //moveRotate = false;

                //rigid.MovePosition(gameObject.transform.position + forward * WALK_SPEED * (moveRight + moveLeft));

                gameObject.transform.position += forward * WALK_SPEED * (moveRight + moveLeft); // �̵�

                isMoveForward = true;
                maxSpeed = Mathf.Max(maxSpeed, moveRight + moveLeft);
                force = WALK_SPEED * maxSpeed;
            }
            // õõ�� ����
            else if (isMoveForward)
            {
                swimmingSound.Stop();
                // StopCoroutine("MoveForwardCoroutine");
                // Debug.Log("moveForward");
                //moveForward = false;
                //StartCoroutine("MoveForwardCoroutine", WALK_SPEED * 0.02f);

                //rigid.MovePosition(gameObject.transform.position + forward * force);

                gameObject.transform.position += forward * force;   // �̵�
                //Debug.Log("MoveForwardCoroutine " + forward * force);
                maxSpeed = 0;   // �ִ� �ӵ� �ʱ�ȭ
                force = Mathf.Lerp(force, 0, Time.deltaTime);   // 

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
    }

    // ���� ���Ŀ��� �������� ���ϰ�
    IEnumerator MoveDelayCoroutine()
    {
        yield return new WaitForSeconds(MOVE_DELAY);

        befRight = rightController.transform.localPosition.x;
        befLeft = leftController.transform.localPosition.x;
        canMove = true;
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
    }

    protected IEnumerator VibrateControllerEnd(float waitTime, float frequency, float amplitude, OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, controller);
        yield return new WaitForSeconds(waitTime);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.All);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bubbles")
        {
            StartCoroutine(VibrateController(0.05f, 0.3f, 0.2f, OVRInput.Controller.All));
        } else if (other.name == "Jellyfish")
        {
            other.GetComponent<AudioSource>().Play();
            StartCoroutine(VibrateControllerEnd(0.05f, 0.1f, 0.4f, OVRInput.Controller.All));
            new WaitForSeconds(0.1f);
            StartCoroutine(VibrateControllerEnd(0.05f, 0.1f, 0.4f, OVRInput.Controller.All));
            new WaitForSeconds(0.1f);
            StartCoroutine(VibrateControllerEnd(0.05f, 0.1f, 0.4f, OVRInput.Controller.All));
        } else if (other.name == "Fishes")
        {
            StartCoroutine(VibrateController(0.05f, 0.4f, 0.6f, OVRInput.Controller.All));
        } else if (other.name == "MoonCrystal")
        {
            Data.Instance.isFindCrystal = true;
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Bubbles" || other.name == "Fishes")
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.All);
        } else if (other.name == "Jellyfish")
        {
            other.GetComponent<AudioSource>().Stop();
        }
    }
}