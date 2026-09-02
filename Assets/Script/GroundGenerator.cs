using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 地面生成させるクラス
/// </summary>
public class GroundGenerator : MonoBehaviour
{
    [Header("参照")]
    [Tooltip("参照する地面リスト一覧")]
    [SerializeField] private List<GameObject> _groundPrefabs = new List<GameObject>();
    [SerializeField] private GroundPool _groundPool;

    [Header("地面設定")]
    [Tooltip("地面生成のスタートポジション")]
    [SerializeField] private Transform _startPosition;
    [Tooltip("地面削除ポジション")]
    [SerializeField] private Transform _endPosition;

    [Header("初期地面生成数")]
    [Tooltip("地面生成数")]
    [SerializeField] private int _spawnCount = 10;
    [Tooltip("地面生成間隔")]
    [SerializeField] private Vector3 _spawnDirection;

    //現在生成した地面オブジェクト
    private GameObject _nowGround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateSpacedGrounds();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGround();
    }

    /// <summary>
    /// 初期地面生成装置
    /// </summary>
    void GenerateSpacedGrounds()
    {
        if (_groundPrefabs == null || _groundPrefabs.Count == 0)
        {
            Debug.LogError("プレハブが登録されていません！");
            return;
        }

        Vector3 currentPosition = transform.position;

        for (int i = 0; i < _spawnCount; i++)
        {
            // リストからランダムにプレハブを選択
            GameObject prefab = _groundPrefabs[UnityEngine.Random.Range(0, _groundPrefabs.Count)];

            // 1. プレハブの大きさを自動取得
            Vector3 groundSize = GetGroundSize(prefab);

            // 2. 2個目以降は、前の地面の大きさ分だけ位置をずらす
            if (i > 0)
            {
                // 進む方向（_spawnDirection）の軸に合わせてサイズ分を加算
                currentPosition.x += groundSize.x * _spawnDirection.x;
                currentPosition.y += groundSize.y * _spawnDirection.y;
                currentPosition.z += groundSize.z * _spawnDirection.z;
            }

            // 3. 計算した位置に生成
            GameObject spawnedGround = Instantiate(prefab, currentPosition, Quaternion.identity);
            spawnedGround.transform.SetParent(this.transform);
        }
    }

    // 地面のプレハブからサイズ（Bounds）を自動計算するメソッド
    Vector3 GetGroundSize(GameObject prefab)
    {
        // Colliderがついている場合はそのサイズを取得
        Collider col = prefab.GetComponent<Collider>();
        if (col != null)
        {
            return col.bounds.size;
        }

        // Rendererがついている場合はそのサイズを取得
        Renderer rend = prefab.GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            return rend.bounds.size;
        }

        // どちらもない場合のデフォルトサイズ（必要に応じて変更）
        Debug.LogWarning($"{prefab.name} に Collider や Renderer が見つからないため、デフォルトサイズ(10, 1, 10)を使用します。");
        return new Vector3(10f, 1f, 10f);
    }

/// <summary>
/// prefabリストから使用していない地面を選択し自動生成する処理
/// </summary>
private void UpdateGround()
    {
        for (int i = 0; i < _groundPrefabs.Count; i++)
        {
            if (_nowGround != _groundPrefabs[i])
            {
                _groundPrefabs[i].SetActive(true);
                _nowGround = _groundPrefabs[i];
            }
            else if (_nowGround == _groundPrefabs[i])
            {
                continue;
            }
        }
    }

    /// <summary>
    /// 地面生成処理
    /// </summary>
    private void GenerateGround()
    {
        _groundPool?.GetGround();
    }

    /// <summary>
    /// 地面削除処理
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _groundPool?.GetGround();
            _groundPool?.ReturnGround(collision.gameObject);
        }
    }
}
