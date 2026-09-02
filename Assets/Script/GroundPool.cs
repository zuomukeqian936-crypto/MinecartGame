using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class GroundPool : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] private List<GameObject> _groundPrefab;

    [Header("地面生成数")]
    [SerializeField] private int _groundCount = 20;

    [Header("地面生成場所")]
    [SerializeField] private Vector3 _spawnPosition;

    private Queue<GameObject> _groundPool = new Queue<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializePool();
    }

    //オブジェクトプールを生成する処理
    private void InitializePool()
    {
        if (_groundPrefab == null || _groundPrefab.Count == 0)
        {
            Debug.LogError("Ground Prefabがリストに登録されていません！");
            return;
        }

        for (int i = 0; i < _groundCount; i++)
        {
            // リストの中からランダムに、または順番にプレハブを選択
            GameObject prefabToSpawn = _groundPrefab[UnityEngine.Random.Range(0, _groundPrefab.Count)];

            // 生成（位置は必要に応じて後からずらす、または初期位置に配置）
            GameObject ground = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            ground.SetActive(false);

            // プールに格納する
            _groundPool.Enqueue(ground);
        }
    }

    // プールから地面を取り出すメソッドの例
    public GameObject GetGround()
    {
        if (_groundPool.Count > 0)
        {
            GameObject ground = _groundPool.Dequeue();
            ground.transform.position = _spawnPosition;
            ground.SetActive(true);
            return ground;
        }
        else
        {
            GameObject prefabToSpawn = _groundPrefab[UnityEngine.Random.Range(0, _groundPrefab.Count)];
            GameObject ground = Instantiate(prefabToSpawn, _spawnPosition, Quaternion.identity);
            return ground;
        }
    }

    // 使い終わった地面をプールに戻すメソッドの例
    public void ReturnGround(GameObject ground)
    {
        ground.SetActive(false);
        _groundPool.Enqueue(ground);
    }
}
