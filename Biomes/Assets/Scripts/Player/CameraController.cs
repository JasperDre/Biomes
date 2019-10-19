using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum Mode
    {
        Orbiting = 0,
        Following = 1,
        Flying = 2,
    }

    [SerializeField] private Mode myMode;
    [SerializeField] private KeyCode mySwitchModeKey;

    [Header("Orbit")]
    [SerializeField] private Transform myTerrain;
    [SerializeField] private Vector3 myRotationAxis;
    [SerializeField] private float myRotationSpeed = 1.0f;

    [Header("Fly")]
    [SerializeField] private float mySpeed = 100.0f;
    [SerializeField] private float mySpeedMultiplier = 250.0f;
    [SerializeField] private float myMaxSpeed = 1000.0f;
    [SerializeField] private float mySensitivity = 0.25f; 

    private Vector3 myLastMousePosition = new Vector3(255, 255, 255);
    private float myTotalMultiplied = 1.0f;
    private Transform myFocusPoint;

    public void StartFollowing(Transform aTransform)
    {
        SetFocusPoint(aTransform);
        myMode = Mode.Following;
    }

    public void SetFocusPoint(Transform aTransform)
    {
        myFocusPoint = aTransform;
    }

    private void Update()
    {
        if (Input.GetKeyUp(mySwitchModeKey))
        {
            switch (myMode)
            {
                case Mode.Orbiting:
                    myMode = Mode.Flying;
                    break;
                case Mode.Flying:
                    myMode = Mode.Orbiting;
                    break;
                default:
                    Debug.LogWarning("Mode is undefined");
                    break;
            }
        }

        switch (myMode)
        {
            case Mode.Orbiting:
                UpdateOrbitingBehavior();
                break;
            case Mode.Following:
                UpdateFollowingCamera();
                break;
            case Mode.Flying:
                UpdateFlyingBehavior();
                break;
            default:
                Debug.LogWarning("Mode is undefined");
                break;
        }
    }

    private void UpdateOrbitingBehavior()
    {
        myFocusPoint = myTerrain;
        transform.LookAt(myFocusPoint.position);
        transform.RotateAround(myFocusPoint.position, myRotationAxis, myRotationSpeed * Time.unscaledDeltaTime);
    }

    private void UpdateFollowingCamera()
    {
        transform.position = myFocusPoint.position + new Vector3(0.0f, 5.0f, 5.0f);
        transform.LookAt(myFocusPoint.position);
    }

    private void UpdateFlyingBehavior()
    {
        myLastMousePosition = Input.mousePosition - myLastMousePosition;
        myLastMousePosition = new Vector3(-myLastMousePosition.y * mySensitivity, myLastMousePosition.x * mySensitivity, 0);
        myLastMousePosition = new Vector3(transform.eulerAngles.x + myLastMousePosition.x, transform.eulerAngles.y + myLastMousePosition.y, 0);
        transform.eulerAngles = myLastMousePosition;
        myLastMousePosition = Input.mousePosition;

        Vector3 inputAxes = GetBaseInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            myTotalMultiplied += Time.unscaledDeltaTime;
            inputAxes = inputAxes * myTotalMultiplied * mySpeedMultiplier;
            inputAxes.x = Mathf.Clamp(inputAxes.x, -myMaxSpeed, myMaxSpeed);
            inputAxes.y = Mathf.Clamp(inputAxes.y, -myMaxSpeed, myMaxSpeed);
            inputAxes.z = Mathf.Clamp(inputAxes.z, -myMaxSpeed, myMaxSpeed);
        }
        else
        {
            myTotalMultiplied = Mathf.Clamp(myTotalMultiplied * 0.5f, 1f, 1000f);
            inputAxes = inputAxes * mySpeed;
        }

        inputAxes = inputAxes * Time.unscaledDeltaTime;
        Vector3 newPosition = transform.position;
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(inputAxes);
            newPosition.x = transform.position.x;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
        else
        {
            transform.Translate(inputAxes);
        }
    }

    private Vector3 GetBaseInput()
    {
        Vector3 velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity += new Vector3(1, 0, 0);
        }
        return velocity;
    }
}