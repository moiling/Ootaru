using UnityEngine;

namespace CardMode.Scripts {
    public class TransparentWall : MonoBehaviour {
        public GameObject Player;

        private GameObject _lastObject;

        private void Update() {
            // Debug.DrawLine(Player.transform.position, transform.position, Color.red);

            RaycastHit hit;

            if (Physics.Linecast(Player.transform.position, transform.position, out hit)) {

                _lastObject = hit.collider.gameObject;

                var nameTag = _lastObject.tag;

                if (nameTag == "MainCamera" || nameTag == "terrain") return;

                Debug.Log(_lastObject);
                var objColor = _lastObject.GetComponent<SpriteRenderer>().color;

                objColor.a = 0.2f;

                _lastObject.GetComponent<SpriteRenderer>().color = objColor;

            } //还原

            else if (_lastObject != null) {

                var objColor = _lastObject.GetComponent<SpriteRenderer>().color;

                objColor.a = 1.0f;

                _lastObject.GetComponent<SpriteRenderer>().color = objColor;
                _lastObject = null;
            }

        }
    }
}