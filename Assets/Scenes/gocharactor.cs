using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class gocharactor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public StayCharactor stayCharactor;
    public walkplayer walkCharactor;

    [Header("もう一人のキャラクター（サブ）の設定")]
    public Transform subTransform;              // サブキャラの Transform
    public SpriteRenderer subSpriteRenderer;    // サブキャラの SpriteRenderer
    public StayCharactor subStayCharactor;      // サブキャラの StayCharactor
    public walkplayer subWalkCharactor;         // サブキャラの walkplayer

    private bool isright;
    private bool isleft;

    public float moveX = 0f;
    public float movespeed = 5.5f;

    // UIボタンから呼び出すメソッド
    public void rightdown()
    {
        isright = true;
    }

    public void rightup()
    {
        isright = false;
    }

    public void leftdown()
    {
        isleft = true;
    }

    public void leftup()
    {
        isleft = false;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveX = 0;   // 初期化

        if (isleft)
        {
            moveX = -1f;

            // メインキャラの状態更新
            spriteRenderer.flipX = true;
            stayCharactor.enabled = false;
            walkCharactor.enabled = true;

            // サブキャラも同じ状態にする
            if (subSpriteRenderer != null) subSpriteRenderer.flipX = true;
            if (subStayCharactor != null) subStayCharactor.enabled = false;
            if (subWalkCharactor != null) subWalkCharactor.enabled = true;
        }
        else if (isright)
        {
            moveX = 1f;

            // メインキャラの状態更新
            spriteRenderer.flipX = false;
            stayCharactor.enabled = false;
            walkCharactor.enabled = true;

            // サブキャラも同じ状態にする
            if (subSpriteRenderer != null) subSpriteRenderer.flipX = false;
            if (subStayCharactor != null) subStayCharactor.enabled = false;
            if (subWalkCharactor != null) subWalkCharactor.enabled = true;
        }
        else
        {
            // メインキャラの状態更新（停止中）
            stayCharactor.enabled = true;
            walkCharactor.enabled = false;

            // サブキャラも同じ状態にする
            if (subStayCharactor != null) subStayCharactor.enabled = true;
            if (subWalkCharactor != null) subWalkCharactor.enabled = false;
        }

        // 移動量の計算
        Vector3 moveVector = new Vector3(moveX, 0f, 0f) * movespeed * Time.deltaTime;

        // メインキャラを移動
        //transform.Translate(moveVector);

        // サブキャラも同じ移動量だけ動かす（位置は別々、動きは同期）
        if (subTransform != null)
        {
           subTransform.Translate(moveVector);
        }
    }
}