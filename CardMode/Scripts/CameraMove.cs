using UnityEngine;

namespace CardMode.Scripts {
    public class CameraMove : MonoBehaviour {
        public GameObject Player;
        public float MoveSpeed;

        // Use this for initialization
        void Start() { }

        // Update is called once per frame
        void Update() {
        
            // TODO 摄像机角度变化，为什么会旋转到Z坐标
            //Zoom out
            if (Input.GetAxis("Mouse ScrollWheel") < 0) {
                if (GetComponent<Camera>().fieldOfView < 120) {
                    GetComponent<Camera>().fieldOfView += 2;
                    // 角度跟随fieldOfView变化，从0度到80度，对应fieldOfView的40-120
                    transform.RotateAround(transform.position, Vector3.right, 2);
                    //transform.Rotate(2, 0, 0, Space.World); 
                }

                if (GetComponent<Camera>().orthographicSize <= 20)
                    GetComponent<Camera>().orthographicSize += 0.5F;
            } else if (Input.GetAxis("Mouse ScrollWheel") > 0) { //Zoom in
                if (GetComponent<Camera>().fieldOfView > 40) {
                    GetComponent<Camera>().fieldOfView -= 2;
                    // 角度跟随fieldOfView变化，从0度到80度，对应fieldOfView的40-120
                    transform.RotateAround(transform.position, Vector3.right, -2);
                }

                if (GetComponent<Camera>().orthographicSize >= 1)
                    GetComponent<Camera>().orthographicSize -= 0.5F;
            } else if (Input.GetMouseButton(1)) { // 右键
                var x = Input.GetAxis("Mouse X");
                transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * MoveSpeed * x);
               // transform.Rotate(0, Time.deltaTime * MoveSpeed * x, 0, Space.World);
            }


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