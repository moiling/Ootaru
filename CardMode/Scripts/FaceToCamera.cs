using UnityEngine;

namespace CardMode.Scripts {
	public class FaceToCamera : MonoBehaviour {

		private Camera _camera;
		private Vector3 _lastEulerAngles = Vector3.zero;
		
		private void Start () {
			if (Camera.main) {
				_camera = Camera.main;
			}
		}
	
		private void Update () {
			if (!_camera) return;

			var eulerAngles = new Vector3(transform.eulerAngles.x, _camera.transform.eulerAngles.y,
				transform.eulerAngles.z);

			if (eulerAngles == _lastEulerAngles) return;

			transform.eulerAngles = eulerAngles;
			_lastEulerAngles = eulerAngles;
		}
	}
}
