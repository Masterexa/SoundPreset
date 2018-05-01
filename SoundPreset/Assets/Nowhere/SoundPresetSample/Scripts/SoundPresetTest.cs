using UnityEngine;
using System.Collections;
using NowhereUnity.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundPresetTest : MonoBehaviour {

	/* Instance */
		/* Fields */
			public SoundPreset	preset;
			AudioSource			audioSource;


		/* Methods */
			// Use this for initialization
			void Awake()
			{
				audioSource = GetComponent<AudioSource> ();
			}
			
			// Update is called once per frame
			public void Play()
			{
				audioSource.PlayOneShot(preset);
			}
}
