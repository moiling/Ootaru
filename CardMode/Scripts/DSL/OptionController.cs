using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour {

	public string TextStr = "";
	public int optionId;

	private string _lastTextStr = "";
	private Text _text;
	private DialogController _controller;
	
	// Use this for initialization
	void Start () {
		_text = GetComponentInChildren<Text>();
		_controller = DialogController.GetInstance();
	}
	
	// Update is called once per frame
	void Update () {
		if (TextStr != _lastTextStr) {
			_text.text = TextStr;
			_lastTextStr = TextStr;
		}
	}

	public void Click() {
		_controller.SelectOption(optionId);
	}
}

