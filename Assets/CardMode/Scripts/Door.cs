using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public Door Other;
	public GameObject Player;

	private bool _stay;

	private void Update () {
		if (_stay) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				var difference = Player.transform.position - transform.position;
				var destination = Other.transform.position + difference;
				Player.transform.position = destination;
			}
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject == Player) {
			_stay = true;
			GetComponentInChildren<Animator>().SetBool("Open", true);
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject == Player) {
			_stay = false;
			GetComponentInChildren<Animator>().SetBool("Open", false);
		}
	}
}
