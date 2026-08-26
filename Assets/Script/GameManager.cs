using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("参照先")]
    [SerializeField] private GameState _gameState;

    private GameState nowGame = GameState.None;
    private GameState nextGame = GameState.MainGame;
    

    // Update is called once per frame
    void Update()
    {
        switch (nowGame)
        {
            case GameState.TitleGame:
                UpdateTitleGame();
                break;

            case GameState.MainGame:
                UpdateMainGame();
                break;

            case GameState.ResultGame:
                UpdateResultGame();
                break;
        }
        ChangeStats();
    }

    /// <summary>
    /// タイトルシーン更新処理
    /// </summary>
    private void UpdateTitleGame()
    {

    }

    /// <summary>
    /// メインゲーム更新処理
    /// </summary>
    private void UpdateMainGame()
    {

    }

    /// <summary>
    /// リザルトシーン更新処理
    /// </summary>
    private void UpdateResultGame()
    {

    }

    /// <summary>
    /// シーンの変更処理
    /// </summary>
    private void ChangeStats()
    {
        if(nextGame != GameState.None)
        {
            nowGame = nextGame;
            nextGame = GameState.None;
        }
    }
}
