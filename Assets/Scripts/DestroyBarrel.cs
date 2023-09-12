using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBarrel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BarrelDrop"))
        {
            if (other.GetComponentInChildren<Barrel>() != null)
            {
                other.GetComponentInChildren<Barrel>().BarrelDropDestroy();
            }
            other.GetComponent<Rigidbody2D>().gravityScale = 0;
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Destroy(gameObject);
        }
    }
}
