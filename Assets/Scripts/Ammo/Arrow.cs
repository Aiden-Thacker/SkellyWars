using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(transform.GetComponent<Rigidbody>());
    }
}
