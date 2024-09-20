using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceUnit : MonoBehaviour
{
    public GameObject unit;
    public float limit = 15;
    public float placeableLimit = 0;

    private Camera cam;
    [SerializeField]
    private LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the limit has been reached
        if (placeableLimit >= limit)
        {
            Debug.Log("Unit limit reached. Cannot place more units.");
            return;
        }

        // Raycasting from the camera to mouse position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.yellow);

        // Check if ray hit a placeable area
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            // If left mouse button is clicked and hits a valid surface
            if (Input.GetButtonDown("Fire1"))
            {
                // Instantiate the unit at the hit point
                Instantiate(unit, hit.point, Quaternion.identity);
                
                // Increment the unit count
                placeableLimit++;
            }
        }
    }
}
