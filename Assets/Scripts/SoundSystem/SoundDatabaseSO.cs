using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
	fileName = "SoundDB_",
	menuName = "Sound/SoundDatabaseSO"
)]
public class SoundDatabaseSO : ScriptableObject
{
	public List<AudioData> audioDatas = new List<AudioData>();

	private void OnValidate()
	{
		if (audioDatas.Count > 0)
		{
			foreach (var audioData in audioDatas)
			{
				if (audioData.audioClip != null)
				{
					// audioData.audioClipName = audioData.audioClip.name;
					audioData.audioClipDUration = audioData.audioClip.length;
				}
			}
		}
	}

	public AudioClip GetAudioClip(string audioName)
	{
		if (audioDatas.Count > 0)
		{
			foreach (var audioData in audioDatas)
			{
				if (audioData.audioClipName == audioName)
				{
					return audioData.audioClip;
				}
			}
		}
		else
		{
			Debug.LogError("There is no audio clip in Audio Database");
			return null;
		}
		return null;
	}
}

[Serializable]
public class AudioData
{
	public AudioClip audioClip;
	public string audioClipName;
	public float audioClipDUration;
}
