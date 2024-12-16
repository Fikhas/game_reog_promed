using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMovement : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField] float force;
    float timer;
    float delay = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        Vector3 direct = direction.normalized;
        rb.velocity = new Vector2(direct.x, direct.y) * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay)
        {
            Destroy(gameObject);
            timer = 0;
        }
    }
}
