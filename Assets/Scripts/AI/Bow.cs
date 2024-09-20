using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public float timer = 5.0f;
    public Arrow arrow;
    public GameObject prefab;
    public float bulletTime;
    public Transform firePoint;

    void Update()
    {
        ShootAtPlayer();
    }

    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;

        bulletTime = timer;

        GameObject bulletObj = Instantiate(prefab, firePoint.transform.position, firePoint.transform.rotation);
        Rigidbody rb = bulletObj.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * arrow.speed;
    }
}
