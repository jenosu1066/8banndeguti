using UnityEngine;



public class PhotoLoopManager : MonoBehaviour
{
    [Header("プレイヤーのTransform")]
    public Transform player;

    [Header("横に並べた4枚の写真のTransform（左から順に）")]
    public Transform[] photos = new Transform[4];

    [Header("写真1枚あたりの横幅")]
    public float photoWidth = 1000f;

    void Update()
    {
        if (player == null || photos == null || photos.Length == 0) return;

        // 4枚の写真それぞれの位置をチェックし、プレイヤーから離れすぎたら反対側に移動させる
        for (int i = 0; i < photos.Length; i++)
        {
            // プレイヤーが写真の右側へ通り過ぎた場合（右方向へ進んでいる時）
            // ※「写真のX座標 + 写真の幅 * 2」よりもプレイヤーが右に行ったら、一番左側にあった写真を一番右に持ってくる
            if (player.position.x - photos[i].position.x > photoWidth * 2f)
            {
                // 一番右にある写真のX座標を探す
                float maxX = -999f;
                foreach (Transform p in photos)
                {
                    if (p.position.x > maxX) maxX = p.position.x;
                }

                // 通り過ぎた写真を、一番右側のさらに右隣（隣接する位置）へワープさせる
                Vector3 pos = photos[i].position;
                pos.x = maxX + photoWidth;
                photos[i].position = pos;
            }
            // プレイヤーが写真の左側へ通り過ぎた場合（左方向へ戻っている時）
            else if (photos[i].position.x - player.position.x > photoWidth * 2f)
            {
                // 一番左にある写真のX座標を探す
                float minX = 99999f;
                foreach (Transform p in photos)
                {
                    if (p.position.x < minX) minX = p.position.x;
                }

                // 通り過ぎた写真を、一番左側のさらに左隣（隣接する位置）へワープさせる
                Vector3 pos = photos[i].position;
                pos.x = minX - photoWidth;
                photos[i].position = pos;
            }
        }
    }
}