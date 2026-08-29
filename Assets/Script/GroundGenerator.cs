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

    [Header("地面設定")]
    [Tooltip("地面の生成枚数")]
    [SerializeField] private int _groundCount = 10;
    [Tooltip("地面生成のスタートポジション")]
    [SerializeField] private Transform _startPosition;
    [Tooltip("地面削除ポジション")]
    [SerializeField] private Transform _endPosition;

    //現在生成した地面オブジェクト
    private GameObject _nowGround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateGround();
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
        foreach(var prefab in _groundPrefabs)
        {
            if (prefab != _nowGround)
            {
                Instantiate(prefab,transform.position, Quaternion.identity);
            }
        }   
    }
}
