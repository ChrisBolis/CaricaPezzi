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
    float distance;

    // Variabili per highlighting
    [SerializeField] string ObjectName;
    private Color highlightColor;
    Material originalMaterial, tempMaterial;
    Renderer rend = null;

    void Start()
    {
        highlightColor = Color.yellow;
    }

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        xAxisClamp = 0f;
    }

    Transform transf;
    // Update is called once per frame
    void Update()
    {
        CameraRotation();

        RaycastHit hit;
        Renderer currRend;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            currRend = hit.collider.gameObject.GetComponent<Renderer>();
            transf = hit.collider.gameObject.GetComponent<Transform>();

            if (currRend == rend)
                return;

            if (currRend && currRend != rend)
                if (rend)
                    rend.sharedMaterial = originalMaterial;

            if (currRend)
                rend = currRend;

            else
                return;

            originalMaterial = rend.sharedMaterial;

            tempMaterial = new Material(originalMaterial);
            rend.material = tempMaterial;
            rend.material.color = highlightColor;

            distance = Vector3.Distance(hit.collider.transform.position, transform.position);
            Debug.Log($"Stai guardando {hit}");

            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log($"Cliccato {hit}");
                transf.position = transform.position + new Vector3(0, 0, distance);
            }

        }

        else
        {
            if (rend)
            {
                rend.sharedMaterial = originalMaterial;
                rend = null;
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
