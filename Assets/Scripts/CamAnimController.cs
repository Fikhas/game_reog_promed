using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAnimController : MonoBehaviour
{
    private Animator camAnim;

    private void Start()
    {
        camAnim = GetComponent<Animator>();
    }

    public void animSingoPunch()
    {
        StartCoroutine(animSingoPunchCo());
    }

    private IEnumerator animSingoPunchCo()
    {
        camAnim.SetBool("isSingoPunch", true);
        yield return new WaitForSeconds(.2f);
        camAnim.SetBool("isSingoPunch", false);
    }
}
