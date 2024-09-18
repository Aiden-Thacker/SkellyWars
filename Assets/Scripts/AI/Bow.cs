using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public float timer = 5.0f;
    public GameObject arrow;
    public float bulletTime;
    public Transform firePoint;
    public float arrowSpeed = 20.0f;

    void Update()
    {
        ShootAtPlayer();
    }

    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;

        bulletTime = timer;

        GameObject bulletObj = Instantiate(arrow, firePoint.transform.position, firePoint.transform.rotation);
        arrow.GetComponent<Rigidbody>().AddForce(transform.forward * arrowSpeed, ForceMode.Impulse);
    }
}
