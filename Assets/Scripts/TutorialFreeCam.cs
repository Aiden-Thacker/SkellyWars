using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// A simple free camera to be added to a Unity game object.
///
/// Keys:
///	wasd / arrows	- movement
///	q/e 			- up/down (local space)
///	r/f 			- up/down (world space)
///	pageup/pagedown	- up/down (world space)
///	hold shift		- enable fast movement mode
///	right mouse  	- enable free look
///	mouse			- free look / rotation
///
/// </summary>

public class TutorialFreeCam : MonoBehaviour
{
    /// <summary>
    /// Normal speed of camera movement.
    /// </summary>
    public float movementSpeed = 10f;

    /// <summary>
    /// Speed of camera movement when shift is held down,
    /// </summary>
    public float fastMovementSpeed = 100f;

    /// <summary>
    /// Sensitivity for free look.
    /// </summary>
    public float freeLookSensitivity = 3f;

    /// <summary>
    /// Amount to zoom the camera when using the mouse wheel.
    /// </summary>
    public float zoomSensitivity = 10f;

    /// <summary>
    /// Amount to zoom the camera when using the mouse wheel (fast mode).
    /// </summary>
    //public float fastZoomSensitivity = 50f;

    /// <summary>
    /// Set to true when free looking (on right mouse button).
    /// </summary>
    private bool looking = false;

    public GameObject placeUnitDescritpion;
    public GameObject rightClickInfo;
    public bool unitCheck = false;
    public GameObject cameraMoveInfo;

    private float currentRotationX = 0f;

    void Update()
    {
        var fastMode = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        var movementSpeed = fastMode ? this.fastMovementSpeed : this.movementSpeed;

        //Move Left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = transform.position + (-transform.right * movementSpeed * Time.deltaTime);
            //Debug.Log("Move");
            cameraMoveInfo.SetActive(false);
        }

        //Move Right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = transform.position + (transform.right * movementSpeed * Time.deltaTime);
            //Debug.Log("Move");
            cameraMoveInfo.SetActive(false);
        }

        //Move Forward
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = transform.position + (transform.forward * movementSpeed * Time.deltaTime);
            //Debug.Log("Move");
            cameraMoveInfo.SetActive(false);
        }

        //Move Backwards
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = transform.position + (-transform.forward * movementSpeed * Time.deltaTime);
            //Debug.Log("Move");
            cameraMoveInfo.SetActive(false);
        }

        //Fly Up
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position = transform.position + (transform.up * movementSpeed * Time.deltaTime);
            cameraMoveInfo.SetActive(false);
        }

        //Fly Down
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            transform.position = transform.position + (-transform.up * movementSpeed * Time.deltaTime);
            cameraMoveInfo.SetActive(false);
        }

        //Unsure
        if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.PageUp))
        {
            transform.position = transform.position + (Vector3.up * movementSpeed * Time.deltaTime);
            cameraMoveInfo.SetActive(false);
        }

        //Unsure
        if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.PageDown))
        {
            transform.position = transform.position + (-Vector3.up * movementSpeed * Time.deltaTime);
            cameraMoveInfo.SetActive(false);
        }

        if (looking)
        {
            // Horizontal rotation
            float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * freeLookSensitivity;

            // Vertical rotation (handle clamping and smooth vertical rotation)
            currentRotationX -= Input.GetAxis("Mouse Y") * freeLookSensitivity;
            currentRotationX = Mathf.Clamp(currentRotationX, -85f, 85f);

            // Apply rotations
            transform.localEulerAngles = new Vector3(currentRotationX, newRotationX, 0f);
        }

        if(unitCheck)
        {
            placeUnitDescritpion.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartLooking();
            rightClickInfo.SetActive(false);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            StopLooking();
            placeUnitDescritpion.SetActive(true);
        }
    }

    void OnDisable()
    {
        StopLooking();
    }

    /// <summary>
    /// Enable free looking.
    /// </summary>
    public void StartLooking()
    {
        looking = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Disable free looking.
    /// </summary>
    public void StopLooking()
    {
        looking = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
