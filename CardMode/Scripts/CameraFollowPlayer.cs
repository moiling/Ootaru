using UnityEngine;

namespace CardMode.Scripts {
    public class CameraFollowPlayer : MonoBehaviour {
        public GameObject Player;
        
        private void Update() {
            if (Player == null) return;

            var cameraToPlayerDistance = 6f;
            var playerHeight = 2f;

            var upDownRotateY = playerHeight + cameraToPlayerDistance
                                * Mathf.Sin(transform.rotation.eulerAngles.x * Mathf.PI / 180); // 上下旋转处理后的Y

            var upDownRotateZ = cameraToPlayerDistance
                                * Mathf.Cos(transform.rotation.eulerAngles.x * Mathf.PI / 180); // 上下旋转处理后的Z

            var leftRightRotateX = -Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.PI / 180) *
                                   cameraToPlayerDistance; // 左右旋转处理后的X

            var leftRightRotateZ = -Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.PI / 180) * upDownRotateZ;

            transform.position = Player.transform.position +
                                 new Vector3(leftRightRotateX, upDownRotateY, leftRightRotateZ);
        }
    }
}