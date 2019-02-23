using UnityEngine;

namespace Nonograms.Scripts {
    public class ButtonCopyNonograms : MonoBehaviour {
        public NonogramsCalculater Calculator;

        // Use this for initialization
        void Start() { }

        // Update is called once per frame
        void Update() { }

        public void Click() {
            GUIUtility.systemCopyBuffer = Calculator.NonogramsStr;
        }
    }
}