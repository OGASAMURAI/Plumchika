using UnityEngine;
using System.Collections;

public class MoveEnemy : MonoBehaviour {
    float MoveSpeed; //敵の移動速度

    public void Move(float MoveSpeed)
    {
        GetComponent<Transform>().Translate(Vector2.left * Time.deltaTime * MoveSpeed);
    }

    public float Turn(float MoveSpeed)
    {
        return  -MoveSpeed;
    }
}
