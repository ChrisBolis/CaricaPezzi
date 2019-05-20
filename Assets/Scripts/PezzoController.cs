using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PezzoController : MonoBehaviour
{
    [HideInInspector] public int pezziCaricati;
    // Start is called before the first frame update
    void Start()
    {
        pezziCaricati = 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Caricatore")
        {
            transform.position = collision.gameObject.GetComponentInChildren<Transform>().position + new Vector3(0, (gameObject.GetComponent<Renderer>().bounds.size.y + 0.05f) * pezziCaricati, 0);
            pezziCaricati++;
        }
    }
}
