using UnityEngine;

namespace CardMode.Scripts {
    public class CameraRotate : MonoBehaviour {
        
        public float MoveSpeed;
        
        private void Update() {
            // Rotate
            if (Input.GetMouseButton(1)) { // 右键
                var x = Input.GetAxis("Mouse X");
                transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * MoveSpeed * x);
                // transform.Rotate(0, Time.deltaTime * MoveSpeed * x, 0, Space.World);
            }
        }
    }
}