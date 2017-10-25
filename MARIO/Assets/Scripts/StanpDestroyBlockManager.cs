using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StanpDestroyBlockManager : MonoBehaviour {

    private GameObject gameManager;     //ゲームマネージャー

    public LayerMask blockLayer; //ブロックレイヤー

    private Rigidbody2D rbody; //敵制御用Rigidbody2D
    
    
    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void FixedUpdate()
    {

    }
    //落ちるブロックオブジェクト削除処理
    public void DestroyBlock()
    {
        rbody.velocity = new Vector2(0, 0);
        //コライダーを削除
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        Destroy(boxCollider);
        //落下アニメーション
        Sequence animSet = DOTween.Sequence();
        animSet.Append(transform.DOLocalMoveY(-5.0f, 1.0f).SetRelative());

        Destroy(this.gameObject, 1.2f);
    }
}

