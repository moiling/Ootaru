﻿using UnityEngine;

namespace Nonograms.Scripts {
    public class ButtonGenerateNonograms : MonoBehaviour {
        public NonogramsCalculater Calculator;

        public NonogramsCreator Creator;

        // Use this for initialization
        void Start() { }

        // Update is called once per frame
        void Update() { }

        public void Click() {
            Creator.NumberMap = Calculator.NonogramsStr;
            Creator.GenerateNumber();
        }
    }
}