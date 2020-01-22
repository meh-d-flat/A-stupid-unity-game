using UnityEngine;
using UnityEngine.UI;

public class MenuCanvas : MonoBehaviour
{
    static MenuCanvas instance;
    static bool isPaused;

    GameObject canvas;
    Image panel;
    Button buttonResume, buttonQuit;

    public static bool GamePaused {
        get { return isPaused; }
    }

    public static MenuCanvas Getinstance() {
        return instance != null ? instance : null;
    }

    public void ChangeState()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        isPaused = gameObject.activeSelf;
        Time.timeScale = gameObject.activeSelf ? 0f : 1f;//seems like it does not affect LateUpdate
    }

    void Awake() {
        instance = this;
    }

    void Start()
    {
        canvas = gameObject;
        panel = gameObject.GetComponentInChildren<Image>();
        buttonResume = panel.gameObject.GetComponentsInChildren<Button>()[0];
        buttonQuit = panel.gameObject.GetComponentsInChildren<Button>()[1];

        buttonResume.onClick.AddListener(ChangeState);
        buttonQuit.onClick.AddListener(QuitButton);
        canvas.SetActive(false);
    }

    void QuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
