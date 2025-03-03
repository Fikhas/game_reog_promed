using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Fungus;
using UnityEngine;

public class GuidePath : MonoBehaviour
{
	[SerializeField] private GameObject guideObject;
	[SerializeField] private float guideDuration;
	[SerializeField] private bool isStopWithTime;

	public void PlayGuide()
	{
		guideObject.SetActive(true);

		if (playGuideCo != null)
		{
			StopCoroutine(playGuideCo);
			playGuideCo = null;
		}

		if (isStopWithTime)
		{
			playGuideCo = StartCoroutine(PlayGuideCo());
		}
		else
		{
			return;
		}

	}

	public void StopGuide()
	{
		guideObject.SetActive(false);

		if (playGuideCo != null)
		{
			StopCoroutine(playGuideCo);
			playGuideCo = null;
		}
	}

	private Coroutine playGuideCo;
	private IEnumerator PlayGuideCo()
	{
		yield return new WaitForSeconds(guideDuration);
		guideObject.SetActive(false);
	}
}
