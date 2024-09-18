using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceUnit : MonoBehaviour
{
    [SerializeField]
    private float length = 20.0f;
    [SerializeField]
    private float xDis = 500.0f;
    [SerializeField]
    private float yDis = 500.0f;
    private Vector3 pos;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        pos = new Vector3(xDis,yDis,0);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(pos);
        Debug.DrawRay(ray.origin, ray.direction * length, Color.yellow);
    }
}
