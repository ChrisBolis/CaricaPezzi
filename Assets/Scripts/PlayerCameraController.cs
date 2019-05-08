using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] float mouseSensivity;

    [SerializeField] Transform playerBody;

    [SerializeField] Camera fpsCam;
    [SerializeField] float range = 100f;

    float xAxisClamp;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        xAxisClamp = 0f;
    }

    Rigidbody rb;
    // Update is called once per frame
    void Update()
    {
        CameraRotation();

        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            rb = rb.GetComponent<Rigidbody>();

            if (rb != null)
            {

            }
        }
    }

    float mouseX;
    float mouseY;

    // Gestione input da mouse e relativo movimento della telecamera
    void CameraRotation()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp > 90f)
        {
            xAxisClamp = 90f;
            mouseY = 0f;
            ClampXAxisRotationToValue(270f);
        }

        else if(xAxisClamp < -90f)
        {
            xAxisClamp = -90f;
            mouseY = 0f;
            ClampXAxisRotationToValue(90f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    // Metodo per evitare rotazioni eccessive della telecamera
    void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
