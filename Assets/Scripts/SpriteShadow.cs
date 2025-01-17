﻿using UnityEngine;
using UnityEngine.Rendering;

namespace Scripts {
    public class SpriteShadow : MonoBehaviour {
        public bool ShadowOnly;

        void OnEnable() {

            transform.GetComponent<SpriteRenderer>().receiveShadows = true;

            transform.GetComponent<SpriteRenderer>().shadowCastingMode =
                ShadowOnly ? ShadowCastingMode.ShadowsOnly : ShadowCastingMode.On;
        }
    }
}