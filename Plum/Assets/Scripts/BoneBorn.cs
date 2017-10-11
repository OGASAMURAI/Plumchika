using UnityEngine;
using System.Collections;

public class BoneBorn : MonoBehaviour {
	public Rigidbody2D rb2D;
	public float MoveSpeed = 0.5f; 	//敵の移動速度
	public bool deadState = false;		//仮死判定
	public int rebornCount;			//復活までのカウントダウン
    MoveEnemy MoveEnemy;

	SpriteRenderer Sr;
	public Sprite natural;
	public Sprite deadSp;

	void Start () {
        MoveEnemy = GetComponent<MoveEnemy>();  //移動用スクリプト
		Sr = gameObject.GetComponent<SpriteRenderer> ();
	}

	void Update () {
		if (deadState != true) {
            MoveEnemy.Move(MoveSpeed);
		} else {
            MoveEnemy.Move(0);
			Sr.sprite = deadSp;
            rebornCount++;
		}
		//一定時間後に復活
		if (rebornCount > 600) {
			deadState = false;
			Sr.sprite = natural;
			rebornCount = 0;
		}
	}
    //衝突判定
    void OnCollisionEnter2D(Collision2D c) {
        if (c.gameObject.tag == "taru")
        {
            deadState = true;
        }else if(c.gameObject.tag == "Enemy")
        {
            MoveSpeed = -MoveSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D cl) {
        if (cl.tag == "Wall")
        {
            MoveSpeed = -MoveSpeed;
        }
    }
}
