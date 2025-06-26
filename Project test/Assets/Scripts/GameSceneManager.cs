using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [Header("场景配置")]
    public string mainMenuScene = "MainMenu";
    public string levelSelectScene = "LevelSelect";
    public string gameScene = "GameScene";

    [Header("音效配置")]
    public AudioSource audioSource;
    public AudioClip buttonClickSound;

    // 单例模式
    public static GameSceneManager Instance { get; private set; }

    void Awake()
    {

    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    #region 场景跳转方法

    public void LoadMainMenu()
    {
        PlayButtonSound();
        SceneManager.LoadScene(mainMenuScene);
    }

    public void LoadLevelSelect()
    {
        PlayButtonSound();
        SceneManager.LoadScene(levelSelectScene);
    }

    public void LoadGameScene()
    {
        PlayButtonSound();
        SceneManager.LoadScene(gameScene);
    }

    public void RestartCurrentScene()
    {
        PlayButtonSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    #endregion

    #region 游戏控制方法

    public void QuitGame()
    {
        PlayButtonSound();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    #endregion

    #region 辅助方法

    private void PlayButtonSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    public bool IsInGameScene()
    {
        return SceneManager.GetActiveScene().name == gameScene;
    }

    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    #endregion
}