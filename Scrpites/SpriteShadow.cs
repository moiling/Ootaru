using UnityEngine;
using UnityEngine.Rendering;

namespace CardMode.Scripts {
	public class SpriteShadow : MonoBehaviour {

		public bool ShadowOnly;

		void OnEnable() {

			transform.GetComponent<SpriteRenderer>().receiveShadows = true;

			if (ShadowOnly) {
				transform.GetComponent<SpriteRenderer>().shadowCastingMode = ShadowCastingMode.ShadowsOnly;
			} else {
				transform.GetComponent<SpriteRenderer>().shadowCastingMode = ShadowCastingMode.On;
			}


		}
	}
}
