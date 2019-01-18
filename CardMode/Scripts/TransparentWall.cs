using UnityEngine;

namespace CardMode.Scripts {
    public class TransparentWall : MonoBehaviour {
        public GameObject Player;

        private GameObject _lastObject;


        void Update() {
            Debug.DrawLine(Player.transform.position, transform.position, Color.red);

            RaycastHit hit;

            if (Physics.Linecast(Player.transform.position, transform.position, out hit)) {

                _lastObject = hit.collider.gameObject;

                string name_tag = _lastObject.tag;

                if (name_tag != "MainCamera" && name_tag != "terrain") {
                    Debug.Log(_lastObject);
                    Color obj_color = _lastObject.GetComponent<SpriteRenderer>().color;

                    obj_color.a = 0.2f;

                    _lastObject.GetComponent<SpriteRenderer>().color = obj_color;
                    //_lastObject.GetComponent<SpriteRenderer>().material.SetColor("Tint", obj_color);

                }

            } //还原

            else if (
                _lastObject != null) {

                Color obj_color = _lastObject.GetComponent<SpriteRenderer>().color;

                obj_color.a = 1.0f;

                _lastObject.GetComponent<SpriteRenderer>().color = obj_color;
                //_lastObject.GetComponent<SpriteRenderer>().material.SetColor("Tint", obj_color);

                _lastObject = null;

            }

        }
    }
}