using UnityEngine;
using UnityEngine.InputSystem;

public class walkplayer : MonoBehaviour
{
    public SpriteRenderer spriteRendererbox;
    public float speedtime = 0.15f;
    public Sprite[] box;
    public float Movetime = 0.0f;
    public int idx = 0;

    [Header("同期するサブキャラのRenderer")]
    public SpriteRenderer subSpriteRendererbox; // ここにサブキャラのSpriteRendererを割り当てる

    void Start()
    {
        this.spriteRendererbox = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        this.Movetime += Time.deltaTime;
        if (this.Movetime > speedtime)
        {
            this.Movetime = 0;

            this.idx = (this.idx + 1) % this.box.Length;

            // メインキャラの画像を更新
            this.spriteRendererbox.sprite = this.box[this.idx];

            // サブキャラが設定されているなら、同じ画像を適用する
            if (subSpriteRendererbox != null && this.idx < this.box.Length)
            {
                subSpriteRendererbox.sprite = this.box[this.idx];
            }
        }
    }
}