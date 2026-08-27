using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地面生成させるクラス
/// </summary>
public class GroundController : MonoBehaviour
{
    [Header("参照")]
    [Tooltip("参照する地面リスト一覧")]
    [SerializeField] private List<GameObject> _groundPrefabs;

    [Tooltip("地面の生成枚数")]
    [SerializeField] private int _groundCount = 10;

    //使用している地面
    private GameObject _nowGround;

    [Tooltip("")]

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

   // private void 
}
