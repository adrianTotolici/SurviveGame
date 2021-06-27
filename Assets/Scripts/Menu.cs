using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public Text startButtonText;
    public Text exitButtonText;
    public Text gameName;


    public void Awake()
    {
        initMenu();
    }

    // Initialization of Menu
    public void initMenu()
    {
        startButtonText = GameObject.Find("TextButtonStart").GetComponent<Text>();
        startButtonText.text = "Start";

        exitButtonText = GameObject.Find("TextButtonExit").GetComponent<Text>();
        exitButtonText.text = "Exit";

        gameName = GameObject.Find("GameName").GetComponent<Text>();
        gameName.text = "Game 1";
    }

    // Start game scene
    public void start_game()
    {
        SceneManager.UnloadSceneAsync("MenuScene");
        SceneManager.LoadSceneAsync("PlayScene", LoadSceneMode.Single);
        Scene playScene = SceneManager.GetSceneByName("PlayScene");
        SceneManager.SetActiveScene(playScene);
    }

    // Exit Game
    public void exit_game()
    {
        print("Exit game");
        Application.Quit();
    }
}
