using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NowhereUnity.Audio {

    [System.Serializable]
	public struct FrequencyRange{
        public float    min;
        public float    max;

        public FrequencyRange(float min, float max) {
            this.min = min;
            this.max = max;
        }
    }
}