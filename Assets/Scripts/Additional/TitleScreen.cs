using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
	[SerializeField] float tssTime;
	[SerializeField] Animator tssAnim;
	[SerializeField] Inventory playerInventory;

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
		playerInventory.numberOfShields = 0;
		playerInventory.numberOfKeys = 0;
		yield return new WaitForSeconds(tssTime);
		SceneManager.LoadScene("GameplayMobile");
	}
}
