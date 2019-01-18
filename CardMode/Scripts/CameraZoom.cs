using UnityEngine;

namespace CardMode.Scripts {
    
    public class CameraZoom : MonoBehaviour {
        
        private void Update() {
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
            }  
            // Zoom in
            if (Input.GetAxis("Mouse ScrollWheel") > 0) { //Zoom in
                if (GetComponent<Camera>().fieldOfView > 40) {
                    GetComponent<Camera>().fieldOfView -= 2;
                    // 角度跟随fieldOfView变化，从0度到80度，对应fieldOfView的40-120
                    transform.RotateAround(transform.position, Vector3.right, -2);
                }

                if (GetComponent<Camera>().orthographicSize >= 1)
                    GetComponent<Camera>().orthographicSize -= 0.5F;
            }
        }
    }
}