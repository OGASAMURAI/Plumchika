using UnityEngine;
using System.Collections;

public class Goomba : MonoBehaviour {
    public float MoveSpeed = 0.5f; 	//敵の移動速度
    MoveEnemy MoveEnemy;

    void Start () {
        MoveEnemy = GetComponent<MoveEnemy>();  //移動用スクリプト
    }

	void Update () {
        MoveEnemy.Move(MoveSpeed);
    }

    //衝突判定
    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "taru")
        {
            Destroy(gameObject);
        }
        else if (c.gameObject.tag == "Enemy")
        {
            MoveSpeed = -MoveSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D cl)
    {
        if (cl.tag == "Wall")
        {
            MoveSpeed = -MoveSpeed;
        }
    }
}
