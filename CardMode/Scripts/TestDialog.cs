using Scripts.DSL;
using UnityEngine;

namespace CardMode.Scripts {
    public class TestDialog : MonoBehaviour {

        public TextAsset Dialog;

        private readonly DslParser _testDialog = new DslParser();
        
        private void Start() {
            Dialog = Resources.Load("TestDialog2") as TextAsset;

            if (Dialog != null) {

                _testDialog.Parse(Dialog.text);
                
            }
        }
    }
}