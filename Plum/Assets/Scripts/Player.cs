using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Rigidbody2D rb2D;
	bool UpcharaTrigger;     //上移動判定
    Transform taruPosL;
    Transform taruPosR;

    public int dir = 1;			//キャラクターの向き 0:left, 1:right
    int dir_bef;            //キャラクターの前回の向き
	public bool taruOverlap;	//背景の樽と重なっているか
	public bool taruState;	//樽持ち状態　true:持ってる
	public bool tarunage;   //	仮変数
	public int havekey = 0;		//鍵の所有数
	public bool muteki;			//無敵状態
	public int mutekiCount;		//無敵時間

    float jumpForce = 450.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;

    public GameObject taru;

    public Animator Anim;
    Vector2 scale;      //Sprite向き取得
	//SpriteRenderer Sr;
	
	void Start () {
		rb2D = GetComponent<Rigidbody2D>();
		//Sr = gameObject.GetComponent<SpriteRenderer> ();
        scale = transform.localScale;
        dir_bef = dir;
        Anim = GetComponent<Animator>();
        Anim.SetBool("isMove", false);
    }
	
	void Update () {
		//ジャンプ
		if (Input.GetKeyDown (KeyCode.Space) && this.rb2D.velocity.y == 0) {
			this.rb2D.AddForce (transform.up * this.jumpForce);
		}
		//移動
		int key = 0;
		if (Input.GetKey (KeyCode.RightArrow)) {
			key = 1;
			dir = 1;

		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			key = -1;
			dir = 0;
		}

        //画像(アニメ)の変更
        //歩行とダッシュ
        if(key != 0)
        {
            Anim.SetBool("isMove", true);
        }
        else
        {
            Anim.SetBool("isMove", false);
        }
        //ジャンプと着地
        //

		//画像の方向転換
		if (dir != dir_bef) {
			//scale.x *= -1;
			//なぜかタルの処理と競合。
			//新しく画像用意するしかない…
		}
		transform.localScale = scale; //代入しなおす
    
		dir_bef = dir;
		float speedx = Mathf.Abs (this.rb2D.velocity.x);

		if (speedx < this.maxWalkSpeed) {
			this.rb2D.AddForce (transform.right * key * this.walkForce);
		}
		
		if (Input.GetKeyDown (KeyCode.K)) {
			//樽を投げる
			if (taruState) {
				//taruPositionの位置でタル発射
				if (dir == 0) {
					taruPosL = transform.FindChild ("taruPosL");
					//Debug.Log("L:" + taruPosL.position.x+" "+taruPosL.position.y);
					Instantiate (taru, new Vector2 (taruPosL.position.x, taruPosL.position.y), Quaternion.identity);
				} else {
					taruPosR = transform.FindChild ("taruPosR");
					//Debug.Log("R:" + taruPosR.position.x + " " + taruPosR.position.y);
					Instantiate (taru, new Vector2 (taruPosR.position.x, taruPosR.position.y), Quaternion.identity);
				}
                Anim.SetBool("ishave", false);
				taruState = false;
				//樽をもつ
			}else if(taruOverlap){
                Anim.SetBool("ishave", true);
				taruState = true;
			}
			//バグ：樽に重なっている状態で、連続で投げることができない
		}
		//無敵継続条件
		if (muteki && mutekiCount < 1800) {
			mutekiCount++;
		} else {
			muteki = false;
		}
	}

    //梯子を下りるための処理
    private void OnTriggerStay2D(Collider2D collision) {
		if (collision.tag == "hasigo") {
			Rigidbody2D r = GetComponent<Rigidbody2D> ();
			//重力相殺
			r.velocity = Vector2.up * 0.6f;

			if (Input.GetKey (KeyCode.UpArrow) && UpcharaTrigger && taruState == false) {
				r.velocity = Vector2.up * 5;
			}
			if (Input.GetKey (KeyCode.DownArrow) && taruState == false) {
				r.velocity = Vector2.down * 5;
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				r.velocity = Vector2.right * 3;
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				r.velocity = Vector2.left * 3;
			}

			if (collision.tag == "Downhantei") {
				UpcharaTrigger = false;
			} else {
				UpcharaTrigger = true;
			}
		}
    }
    private void OnCollisionEnter2D(Collision2D cl)
    {
        if (cl.gameObject.tag == "Enemy")
        {
            if (muteki) { 
                Destroy(cl.gameObject);
            }else
            {
                Destroy(gameObject);
            }
        }
    }

	private void OnTriggerEnter2D(Collider2D collision){
		if (collision.tag == "Item_key") {
			havekey++;
			Destroy (collision.gameObject);
		} else if (collision.tag == "Item_taru") {
			taruOverlap = true;
		} else if (collision.tag == "Item_hummer") {
			muteki = true;
			mutekiCount = 0;
			Destroy (collision.gameObject);
		}
	}
	private void OnTriggerExit2D(Collider2D collision){
		if (collision.tag == "Item_taru") {
			taruOverlap = false;
		}
	}
}