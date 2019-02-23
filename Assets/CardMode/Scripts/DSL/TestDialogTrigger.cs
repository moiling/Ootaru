using UnityEngine;

namespace CardMode.Scripts {
    public class TestDialogTrigger : MonoBehaviour {

        public TextAsset Dialog;
        public GameObject Player;

        private DialogController _controller;
        private bool _stay;
        
        private void Start() {
            Dialog = Resources.Load("TestDialog2") as TextAsset;
            _controller = DialogController.GetInstance();
         
        }

        private void Update() {
            if (_stay) {
                if (Dialog != null && Input.GetKeyDown(KeyCode.E)) {
                    _controller.StartDialog(Dialog.text);
                }   
                
                /*
                if (Input.GetKeyDown(KeyCode.Space)) {
                    _runner.Next();
                }

                if (Input.GetKeyDown(KeyCode.Z)) {
                    _runner.Select(0);
                }

                if (Input.GetKeyDown(KeyCode.X)) {
                    _runner.Select(1);
                }

                if (Input.GetKeyDown(KeyCode.C)) {
                    _runner.Select(2);
                }

                if (Input.GetKeyDown(KeyCode.V)) {
                    Debug.Log(_runner.CurrentIntro);
                }
                */
            }
        }
        
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject == Player) {
                _stay = true;
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.gameObject == Player) {
                _stay = false;
            }
        }
    }
}