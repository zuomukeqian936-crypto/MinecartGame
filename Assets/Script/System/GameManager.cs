using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("参照先")]
    [SerializeField] private GameState _gameState;

    private GameState _creentGame = GameState.None;
    private GameState _nextGame = GameState.MainGame;
    

    // Update is called once per frame
    void Update()
    {
        switch (_creentGame)
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
        SceneManager.LoadScene("MainScene");
    }

    /// <summary>
    /// メインゲーム更新処理
    /// </summary>
    private void UpdateMainGame()
    {
        SceneManager.LoadScene("ResultScene");
    }

    /// <summary>
    /// リザルトシーン更新処理
    /// </summary>
    private void UpdateResultGame()
    {
        SceneManager.LoadScene("TitleScene");
    }

    /// <summary>
    /// シーンの変更処理
    /// </summary>
    private void ChangeStats()
    {
        if(_nextGame != GameState.None)
        {
            _creentGame = _nextGame;
            _nextGame = GameState.None;
        }
    }
}
