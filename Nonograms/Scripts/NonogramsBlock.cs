using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Nonograms.Scripts {
	public class NonogramsBlock : MonoBehaviour {

		public NonogramsType CurrentType = NonogramsType.Clear;
		
		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
			switch (CurrentType) {

				case NonogramsType.Clear:
					GetComponent<Image>().color = Color.white;
					break;
				case NonogramsType.Black:
					GetComponent<Image>().color = Color.black;
					break;
				/*
				case NonogramsType.Gray:
					GetComponent<Image>().color = Color.gray;
					break;
				case NonogramsType.Red:
					GetComponent<Image>().color = Color.red;
					break;
				case NonogramsType.Blue:
					GetComponent<Image>().color = Color.blue;
					break;
				case NonogramsType.Green:
					GetComponent<Image>().color = Color.green;
					break;
				case NonogramsType.Yellow:
					GetComponent<Image>().color = Color.yellow;
					break;
				*/
				//case NonogramsType.Cross:
				//	GetComponent<Image>().color = Color.magenta;
				//	break;
				default:

					throw new ArgumentOutOfRangeException();
			}
		}

		public void Down() {

			CurrentType = (NonogramsType) (((int) CurrentType + 1) % Enum.GetNames(CurrentType.GetType()).Length);
		}

		public void Up() {
			
		}

		public void Enter() {
			if (MouseController.GetInstance().MouseType == -1) { // 未按键跳过
				return;
			}					
			
			CurrentType = (NonogramsType) (((int) CurrentType + 1) % Enum.GetNames(CurrentType.GetType()).Length);
		}
	}
}
