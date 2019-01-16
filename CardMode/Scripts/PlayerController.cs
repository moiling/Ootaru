using UnityEngine;

namespace CardMode.Scripts {
	public class PlayerController : MonoBehaviour {

		public float Speed;
	
		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {

			if (Input.GetKey(KeyCode.S)) {
				
				GetComponent<Rigidbody>().velocity = new Vector3(
					-Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.PI / 180) * Speed,
					0f,
					-Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.PI / 180) * Speed);				
				GetComponent<Animator>().SetBool("WalkForward", true);
				GetComponent<Animator>().SetBool("WalkBack", false);
				GetComponent<Animator>().SetBool("WalkLeft", false);
				GetComponent<Animator>().SetBool("WalkRight", false);
			} else if (Input.GetKey(KeyCode.W)) {
				
				GetComponent<Rigidbody>().velocity = new Vector3(
					Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.PI / 180) * Speed,
					0f,
					Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.PI / 180) * Speed);
				GetComponent<Animator>().SetBool("WalkForward", false);
				GetComponent<Animator>().SetBool("WalkBack", true);
				GetComponent<Animator>().SetBool("WalkLeft", false);
				GetComponent<Animator>().SetBool("WalkRight", false);
			} else if (Input.GetKey(KeyCode.A)) {
				
				GetComponent<Rigidbody>().velocity = new Vector3(
					-Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.PI / 180) * Speed,
					0f,
					Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.PI / 180) * Speed);
				GetComponent<Animator>().SetBool("WalkForward", false);
				GetComponent<Animator>().SetBool("WalkBack", false);
				GetComponent<Animator>().SetBool("WalkLeft", true);
				GetComponent<Animator>().SetBool("WalkRight", false);
			} else if (Input.GetKey(KeyCode.D)) {
				
				GetComponent<Rigidbody>().velocity = new Vector3(
					Mathf.Sin((90 - transform.rotation.eulerAngles.y) * Mathf.PI / 180) * Speed,
					0f,
					-Mathf.Cos((90 - transform.rotation.eulerAngles.y) * Mathf.PI / 180) * Speed);
				GetComponent<Animator>().SetBool("WalkForward", false);
				GetComponent<Animator>().SetBool("WalkBack", false);
				GetComponent<Animator>().SetBool("WalkLeft", false);
				GetComponent<Animator>().SetBool("WalkRight", true);
			} else {
				
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				GetComponent<Animator>().SetBool("WalkForward", false);
				GetComponent<Animator>().SetBool("WalkBack", false);
				GetComponent<Animator>().SetBool("WalkLeft", false);
				GetComponent<Animator>().SetBool("WalkRight", false);
			}
		}
	}
}
