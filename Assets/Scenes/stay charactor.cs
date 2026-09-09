using UnityEngine;

public class StayCharactor : MonoBehaviour
{
    // 2つのキャラクターのSpriteRendererをインスペクターからそれぞれ登録する
    public SpriteRenderer spriteRenderer1;
    public SpriteRenderer spriteRenderer2;

    public float animationspeed = 0.8f;
    public Sprite[] box;

    public float time = 0;
    public int idx = 0;

    void Update()
    {
        if (box == null || box.Length == 0) return;

        this.time += Time.deltaTime;
        if (this.time > animationspeed)
        {
            this.time = 0;

            // インデックスを進める
            this.idx = (this.idx + 1) % this.box.Length;

            // キャラクター1に現在の画像を設定
            if (spriteRenderer1 != null)
            {
                spriteRenderer1.sprite = this.box[this.idx];
            }


            // キャラクター2にはキャラクター1と同じ画像を設定する
            if (spriteRenderer2 != null)
            {
                spriteRenderer2.sprite = this.box[this.idx];
            }
        }

        // キャラクター2には「1つずれた画像（交互）」を設定する
        /* if (spriteRenderer2 != null)
         {
             int alternateIdx = (this.idx + 1) % this.box.Length;
             spriteRenderer2.sprite = this.box[alternateIdx];
         }*/
    }
    }
