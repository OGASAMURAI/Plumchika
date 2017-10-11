using UnityEngine;
using System.Collections;

public class taruController : MonoBehaviour {
	public Rigidbody2D taru2D;
	public float speed = 50.0f;

	public Player PlayerDir;
    GameObject Player;
	public int Tarudir; //樽が動く向き
	
	void Start () {
		taru2D = GetComponent<Rigidbody2D>();
        //オブジェクトを探す
        Player = GameObject.Find("Player");
        PlayerDir = Player.GetComponent<Player>();
        Tarudir = PlayerDir.dir;
        if (Tarudir == 1){
            taru2D.AddForce(transform.right * speed);
        }else{
            taru2D.AddForce(transform.right * -speed);
        }
    }

	void Update(){
		//画面外から出たら削除
		if (transform.position.y<-5.5f){
			Destroy (gameObject);
		}
	}
    private void OnCollisionEnter2D(Collision2D c)
    {
        //敵や壁にぶつかったら消える、割れるアニメーションが欲しいです
        if (c.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D cl)
    {
        if (cl.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
