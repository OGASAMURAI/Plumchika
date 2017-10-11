using UnityEngine;
using System.Collections;
using UnityEngine;

public class Oneup : MonoBehaviour {

	public GameObject namakubi;
		void OnTriggerEnter2D(Collider2D col){
			Destroy(gameObject);
		GameObject director = GameObject.Find ("GameDirector");
		director.GetComponent<GameDirector>().DecreaseHp2();

		}
	
}
