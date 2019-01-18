using UnityEngine;

namespace CardMode.Scripts {
	public class FaceToCamera : MonoBehaviour {

		private Camera _camera;
		// Use this for initialization
		void Start () {
			if (Camera.main) {
				_camera = Camera.main;
			}
		}
	
		// Update is called once per frame
		void Update () {
			//Debug.Log(_camera.transform.localRotation.y);
			if (_camera) {
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, _camera.transform.eulerAngles.y, transform.eulerAngles.z);
			}
			
		}
	}
}
