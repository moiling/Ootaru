using System.Collections;
using System.Collections.Generic;
using Nonograms.Scripts;
using UnityEngine;

public class ButtonResetNonograms : MonoBehaviour {

	public NonogramsCreator Creator;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Click() {
		Creator.NumberMap = "{[[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}]],[[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}],[{0|1}]]}";
		Creator.CreateNonograms();
	}
}
