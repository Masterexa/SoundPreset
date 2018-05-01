using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NowhereUnity.Audio{

	public class SoundPresetSource : MonoBehaviour {

		#region Instance
			#region Fields
                [SerializeField]AudioSource    m_source;
                [SerializeField]SoundPreset    m_preset;
                [SerializeField]bool           m_playOnAwake=true;
			#endregion

			#region Properties
			#endregion

			#region Events
				// Use this for initialization
				void Awake() {
					if( m_source )
                    {
                        m_source.PlayOneShot(m_preset);
                    }
				}
			#endregion

			#region Pipelines
			#endregion

			#region Methods
			#endregion
		#endregion
	}

}