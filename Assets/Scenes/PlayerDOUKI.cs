using UnityEngine;

public class PlayerDOUKI : MonoBehaviour
{
    // 真似する対象（リーダー）のTransformを指定する変数
    public Transform leader;

    // リーダーからどれくらい離れて追従するか（オフセット）
    private Vector3 offset;

    void Start()
    {
        if (leader != null)
        {
            // ゲーム開始時の2人の距離（ズレ）を記憶しておく
            offset = transform.position - leader.position;
        }
    }

    void Update()
    {
        if (leader != null)
        {
            // リーダーの位置にオフセット（最初のズレ）を足した位置に移動する
            // これにより、リーダーの動きを完全に同期して真似できます
            transform.position = leader.position + offset;

            // リーダーの向き（回転）も完全に真似させたい場合は下の行のコメントアウトを外す
            // transform.rotation = leader.rotation;
        }
    }
}