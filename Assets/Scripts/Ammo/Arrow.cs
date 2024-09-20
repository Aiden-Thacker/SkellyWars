using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SuperPupSystems.Helper;

public class Arrow : MonoBehaviour
{
    public int damage = 1;
    public float speed = 20f;
    public float lifeTime = 10f;
    public bool destroyOnImpact = true;
    public UnityEvent hitTarget;
    public LayerMask mask;
    public List<string> tags;

    private Vector3 m_lastPosition;
    private RaycastHit m_info;
    private Timer m_timer;
    private Rigidbody rb;

    private void Awake()
    {
        if (hitTarget == null)
        {
            hitTarget = new UnityEvent();
        }
    }

    private void Start()
    {
        m_timer = GetComponent<Timer>();
        m_timer.timeout.AddListener(DestroyBullet);
        m_timer.StartTimer(lifeTime);

        rb = GetComponent<Rigidbody>(); // Get the Rigidbody

        // Set initial position
        m_lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        // Ensure the arrow continues in the forward direction
        CollisionCheck();
        m_lastPosition = transform.position;
    }

    private void CollisionCheck()
    {
        if (Physics.Linecast(m_lastPosition, transform.position, out m_info, mask))
        {
            if (tags.Contains(m_info.transform.tag))
            {
                // Apply damage to the target
                Debug.Log(m_info.transform.gameObject.name);
                m_info.transform.GetComponent<Health>()?.Damage(damage);
                hitTarget.Invoke();
            }
            // else
            // {
            //     Destroy(transform.GetComponent<Rigidbody>());
            // }

            if (destroyOnImpact)
            {
                DestroyBullet();
            }
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
