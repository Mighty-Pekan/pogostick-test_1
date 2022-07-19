using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoSingleton<GameController>
{
    Player player;
    InputManager inputManager;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft; //or right for right landscape
    }

    public static void GameOver() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    //============================================================== registering objects
    public static void RegisterInputManager(InputManager _inputManager)
    {
        instance.inputManager = _inputManager;
    }
    public static void RegisterPlayer(Player _player) {
        instance.player = _player;
    }
    //==============================================================
    public static void EnableTouchInput()
    {
        instance.inputManager.EnableTouchInput();
    }

    public static void ResetPlayerPosition()
    {
        instance.player.ResetInitialPosition();
    }

    public static int GetLevelsCount()
    {
        return SceneManager.sceneCountInBuildSettings;
    }

    public static void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

}
