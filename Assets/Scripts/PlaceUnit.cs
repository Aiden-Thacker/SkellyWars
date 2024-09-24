using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaceUnit : MonoBehaviour
{
    public GameObject[] units;
    public float limit = 15;
    public float placeableLimit = 0;
    public TMP_Text countText;

    public GameObject highlightOne;
    public GameObject highlightTwo;
    public GameObject highlightThree;

    private Camera cam; 
    [SerializeField]
    private LayerMask layerMask; 
    private GameObject selectedUnit = null; 

    void Start()
    {
        cam = GetComponent<Camera>();

        // Deactivate highlights initially
        highlightOne.SetActive(false);
        highlightTwo.SetActive(false);
        highlightThree.SetActive(false);
    }

    void Update()
    {
        // Check if the unit placement limit has been reached
        if (placeableLimit >= limit)
        {
            Debug.Log("Unit limit reached. Cannot place more units.");
            return;
        }

        // Check for number key inputs (1, 2, or 3) to select a unit
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedUnit = units[0];
            highlightOne.SetActive(true);
            highlightTwo.SetActive(false);
            highlightThree.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedUnit = units[1];
            highlightOne.SetActive(false);
            highlightTwo.SetActive(true);
            highlightThree.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedUnit = units[2];
            highlightOne.SetActive(false);
            highlightTwo.SetActive(false);
            highlightThree.SetActive(true);
        }

        // Raycasting from the camera to mouse position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.yellow);

        // Check if ray hit a placeable area
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (Input.GetButtonDown("Fire1") && selectedUnit != null)
            {
                Instantiate(selectedUnit, hit.point, Quaternion.identity);

                placeableLimit++;
            }
        }

        countText.text = placeableLimit + "/" + limit;
    }
}
