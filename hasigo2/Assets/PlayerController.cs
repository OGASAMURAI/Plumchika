using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    Rigidbody2D rigid2D;
    float jumpForce = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;

    void Start () {

        this.rigid2D = GetComponent<Rigidbody2D>();
	}

	void Update () {

        
		//ジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0)
        {
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

		//移動
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        if(speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

		//画面外に出たとき最初からになる
		if (transform.position.y < -10) {
			//Application.LoadLevel("GameScene"); ゲームシーンを元に戻してしまう。　ゲームオーバー後のやり直しに使えるかも
			transform.position = Vector3.zero;

			GameObject director = GameObject.Find ("GameDirector");
			director.GetComponent<GameDirector>().DecreaseHp();
		}

    }

	//はしご 梯子ボタンを押さないと登れないようにすること
    private void OnTriggerStay2D(Collider2D collision)
    {
		Rigidbody2D r = GetComponent<Rigidbody2D>();
        

       //重力相殺
        r.velocity = Vector2.up * 0.6f;

        if (Input.GetKey(KeyCode.UpArrow)) { 
        r.velocity = Vector2.up * 5;
    }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            r.velocity = Vector2.down * 5;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {          
            r.velocity = Vector2.right * 3;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {            
            r.velocity = Vector2.left * 3;
        }

    }


}