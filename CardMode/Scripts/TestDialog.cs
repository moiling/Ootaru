using Scripts.DSL;
using UnityEngine;

namespace CardMode.Scripts {
    public class TestDialog : MonoBehaviour {

        public TextAsset Dialog;

        private DslRunner _testDialog;
        
        private void Start() {
            Dialog = Resources.Load("TestDialog") as TextAsset;

            if (Dialog != null) {

                _testDialog = new DslRunner(Dialog.text);
                
            }
            
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                _testDialog.Next();
            }

            if (Input.GetKeyDown(KeyCode.Z)) {
                _testDialog.Select(0);
            }
            
            if (Input.GetKeyDown(KeyCode.X)) {
                _testDialog.Select(1);
            }
            
            if (Input.GetKeyDown(KeyCode.C)) {
                _testDialog.Select(2);
            }

            if (Input.GetKeyDown(KeyCode.V)) {
                Debug.Log(_testDialog.CurrentIntro);
            }
        }
    }
}