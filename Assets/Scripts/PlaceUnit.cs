using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    private bool justSelected = false;

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
        if (justSelected)
        {
            justSelected = false;
            return;
        }
        // Check if the unit placement limit has been reached
        if (placeableLimit >= limit)
        {
            Debug.Log("Unit limit reached. Cannot place more units.");
            return;
        }

        // Check for number key inputs (1, 2, or 3) to select a unit
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectUnit(0); 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectUnit(1); 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectUnit(2); 
        }

        // Raycasting from the camera to mouse position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.yellow);

        // Check if ray hit a placeable area
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (Input.GetButtonDown("Fire1") && selectedUnit != null)
            {
                // Check if placing the unit would exceed the limit
                int unitCost = GetUnitCost(selectedUnit);
                if (placeableLimit + unitCost <= limit)
                {
                    Instantiate(selectedUnit, hit.point, Quaternion.identity);
                    PlaceUnitOnGround(unitCost);
                }
                else
                {
                    Debug.Log("Cannot place unit. Limit would be exceeded.");
                    // Later add Text/code that says you hit your limit after trying to keep placing units
                }
            }
        }

        countText.text = placeableLimit + "/" + limit;
    }

    void PlaceUnitOnGround(int unitCost)
    {
        placeableLimit += unitCost;
    }

    int GetUnitCost(GameObject unit)
    {

        if (unit == units[1]) return 3;
        if (unit == units[2]) return 2;
        return 1; // Default cost for unit[0]
    }

    void SelectUnit(int unitIndex)
    {
        selectedUnit = units[unitIndex];
        highlightOne.SetActive(unitIndex == 0);
        highlightTwo.SetActive(unitIndex == 1);
        highlightThree.SetActive(unitIndex == 2);
        justSelected = true;
    }

    public void BasicFriendlyButton() 
    { 
        SelectUnit(0); 
    }
    public void MeleeFriendlyButton() 
    { 
        SelectUnit(1); 
    }
    public void RangeFriendlyButton()
    { 
        SelectUnit(2); 
    }
}
