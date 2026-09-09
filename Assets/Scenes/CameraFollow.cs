using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("追従する対象（未設定の場合は「Player」タグを持つオブジェクトを自動取得）")]
    public Transform target;

    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public float smoothSpeed = 0.125f;

    void Start()
    {
        // Targetが未設定の場合、自動で「Player」タグのオブジェクトを探す
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Toko_Idle_1");
            if (playerObj != null)
            {
                target = playerObj.transform;
            }
            else
            {
                Debug.LogWarning("CameraFollow: 追従するターゲットが見つかりません。「Player」タグを設定するか、インスペクターから指定してください。");
            }
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}