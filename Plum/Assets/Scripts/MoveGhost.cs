using UnityEngine;
using System.Collections;

public class MoveGhost : MonoBehaviour {
	public float MoveSpeed = 0.5f; //敵の移動速度
    public int dir = 1;
    int dir_bef;
    public Vector2 scale;      //Sprite向き取得
    MoveEnemy MoveEnemy;

    void Start () {
        dir_bef = dir;
        scale = transform.localScale;
        MoveEnemy = GetComponent<MoveEnemy>();  //移動用スクリプト
    }

	void Update () {
        MoveEnemy.Move(MoveSpeed);
        if (dir != dir_bef)
        {
            scale.x *= -1;
        }
        transform.localScale = scale; //代入しなおす
        dir_bef = dir;
    }

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "GhostSpace") {
            dir *= -1;
            MoveSpeed = MoveEnemy.Turn(MoveSpeed);
		}
    }
}