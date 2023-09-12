using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] GameObject heart;
    [SerializeField] GameObject smoke;
    [SerializeField] GameObject parentObject;
    [SerializeField] GameObject barrelHitBox;
    private float delay = 0.3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BarrelClue"))
        {
            if (gameObject.GetComponentInParent<BarrelParentScript>() != null)
            {
                gameObject.GetComponentInParent<BarrelParentScript>().isCountToDestroy = true;
            }
        }
        if (other.CompareTag("PlayerSword"))
        {
            smoke.SetActive(true);
            StartCoroutine(HeartCo());
        }
    }

    private IEnumerator HeartCo()
    {
        if (barrelHitBox != null)
        {
            barrelHitBox.SetActive(true);
        }
        yield return new WaitForSeconds(delay);
        if (gameObject.GetComponentInParent<DialogInteractObject>() != null)
        {
            gameObject.GetComponentInParent<DialogInteractObject>().PopUpActive();
        }
        if (heart != null)
        {
            Instantiate(heart, this.transform.position, Quaternion.identity);
        }
        if (barrelHitBox != null)
        {
            barrelHitBox.SetActive(false);
        }
        smoke.SetActive(false);
        Destroy(parentObject);
    }

    public void BarrelDropDestroy()
    {
        smoke.SetActive(true);
        StartCoroutine(HeartCo());
    }
}
