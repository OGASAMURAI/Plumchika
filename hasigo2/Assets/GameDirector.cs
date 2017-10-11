using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour {

	GameObject hpGage;
	GameObject zanki;
	GameObject GameOver;

	int x = 2,flag = 0;
	void Start () {
		this.hpGage = GameObject.Find("hpGage");
		this.zanki = GameObject.Find("zanki");
		this.GameOver = GameObject.Find("GameOver");
		GameOver.SetActive (false);
	}

	void Update () {

		this.zanki.GetComponent<Text>().text =
			"残機　×"+ x;
	
		if (x < 0) {
			GameOver.SetActive (true);
			zanki.SetActive (false);
			this.GameOver.GetComponent<Text>().text =
				"ゲームオーバー";
		}

	}



	public void DecreaseHp(){
		x--;
	}

	public void DecreaseHp2(){
		x++;
	}
}
