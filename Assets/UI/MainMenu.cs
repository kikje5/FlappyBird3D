using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private UIDocument _uiDocument;
    private Button _startButton;
    private Button _settingsButton;
    private Button _exitButton;
    private Button _backToMenuButton;
    private Slider _masterVolumeSlider;
    private Slider _sfxVolumeSlider;
    private Slider _musicVolumeSlider;
    private VisualElement _mainMenu;
    private VisualElement _settingsMenu;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        _startButton = _uiDocument.rootVisualElement.Query<Button>("StartGameButton");
        _settingsButton = _uiDocument.rootVisualElement.Query<Button>("SettingsButton");
        _exitButton = _uiDocument.rootVisualElement.Query<Button>("ExitButton");
        _mainMenu  = _uiDocument.rootVisualElement.Query<VisualElement>("MainMenu");
        _settingsMenu = _uiDocument.rootVisualElement.Query<VisualElement>("SettingsMenu");
        _settingsMenu.style.display = DisplayStyle.None;
        _mainMenu.style.display = DisplayStyle.Flex;
        
        _backToMenuButton =  _uiDocument.rootVisualElement.Query<Button>("BackToMain");
        
        _startButton.RegisterCallback<ClickEvent>(OnStartClick);
        _settingsButton.RegisterCallback<ClickEvent>(OnSettingsClick);
        _exitButton.RegisterCallback<ClickEvent>(OnExitClick);
        _backToMenuButton.RegisterCallback<ClickEvent>(OnBackToMenuClick);
    }
        
    void OnStartClick(ClickEvent evt)
    {
        SceneManager.LoadScene(1);
    }

    void OnSettingsClick(ClickEvent evt)
    {
        _mainMenu.style.display = DisplayStyle.None;
        _settingsMenu.style.display = DisplayStyle.Flex;
    }

    void OnBackToMenuClick(ClickEvent evt)
    {
        _mainMenu.style.display = DisplayStyle.Flex;
        _settingsMenu.style.display = DisplayStyle.None;
    }

    void OnExitClick(ClickEvent evt)
    {
        Application.Quit();
    }
}

