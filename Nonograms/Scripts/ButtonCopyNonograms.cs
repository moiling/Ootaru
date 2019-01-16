using System.Collections;
using System.Collections.Generic;
using Nonograms.Scripts;
using UnityEngine;

public class ButtonCopyNonograms : MonoBehaviour {

	public NonogramsCalculater Calculator;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Click() {
		GUIUtility.systemCopyBuffer = Calculator.NonogramsStr;
	}
}
