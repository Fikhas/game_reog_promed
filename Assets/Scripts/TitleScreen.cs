using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] float tssTime;
    [SerializeField] Animator tssAnim;

    private void Start()
    {
        Time.timeScale = 1;
    }
    public void StartGame()
    {
        tssAnim.Play("Close");
        StartCoroutine(TssCo());
    }

    private IEnumerator TssCo()
    {
        yield return new WaitForSeconds(tssTime);
        SceneManager.LoadScene("SampleScene");
    }
}
