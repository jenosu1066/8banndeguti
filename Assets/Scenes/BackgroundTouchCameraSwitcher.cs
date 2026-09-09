using UnityEngine;

public class CameraPassSwitcher : MonoBehaviour
{
    [Header("1. カメラの設定")]
    [Tooltip("通常のカメラをドラッグ＆ドロップ")]
    public Camera normalCamera;
    [Tooltip("いへんのカメラをドラッグ＆ドロップ")]
    public Camera anomalyCamera;

    [Header("2. 監視するカメラの設定")]
    [Tooltip("背景を通過するかどうかを判定したいカメラ（メインカメラなど）")]
    public Transform targetCameraTransform;

    [Header("3. 判定場所の設定")]
    [Tooltip("範囲を判定したい背景オブジェクト。空ならこのスクリプトがついているオブジェクトになります。")]
    public SpriteRenderer targetBackground;

    [Header("4. 確率の設定 (0〜100%)")]
    [Range(0, 100)]
    public int anomalyChance = 30;

    private bool isNormalState = true;
    private bool wasInside = false;

    void Start()
    {
        if (targetBackground == null)
        {
            targetBackground = GetComponent<SpriteRenderer>();
        }

        if (targetBackground == null)
        {
            Debug.LogError("判定する背景の SpriteRenderer が見つかりません！");
        }

        // 監視するカメラが未設定の場合、自身のカメラ（アタッチされている場合）またはメインカメラを自動取得
        if (targetCameraTransform == null)
        {
            Camera cam = GetComponent<Camera>();
            if (cam != null)
            {
                targetCameraTransform = cam.transform;
            }
            else if (Camera.main != null)
            {
                targetCameraTransform = Camera.main.transform;
            }
        }

        UpdateCameras();
    }

    void Update()
    {
        if (targetBackground == null || targetCameraTransform == null) return;

        Bounds bounds = targetBackground.bounds;
        Vector3 camPos = targetCameraTransform.position;

        // カメラの現在位置が背景の範囲内にあるか判定
        bool isInsideNow = (camPos.x >= bounds.min.x && camPos.x <= bounds.max.x &&
                            camPos.y >= bounds.min.y && camPos.y <= bounds.max.y);

        // 範囲外から範囲内に「入った瞬間」を検知
        if (isInsideNow && !wasInside)
        {
            OnCameraEntered();
        }

        wasInside = isInsideNow;
    }

    // カメラが背景に入ったときの処理
    void OnCameraEntered()
    {
        if (isNormalState)
        {
            int roll = Random.Range(0, 100);
            if (roll < anomalyChance)
            {
                isNormalState = false;
                Debug.Log("【異変カメラに切り替え】");
            }
            else
            {
                Debug.Log("【確率はずれ・通常のまま】");
            }
        }
        else
        {
            isNormalState = true;
            Debug.Log("【通常のカメラに戻りました】");
        }

        UpdateCameras();
    }

    private void UpdateCameras()
    {
        if (normalCamera != null) normalCamera.gameObject.SetActive(isNormalState);
        if (anomalyCamera != null) anomalyCamera.gameObject.SetActive(!isNormalState);
    }
}