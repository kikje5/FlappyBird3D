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
    private SliderInt _masterVolumeSlider;
    private SliderInt _sfxVolumeSlider;
    private SliderInt _musicVolumeSlider;
    private VisualElement _mainMenu;
    private VisualElement _settingsMenu;
    public Settings settings;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        _mainMenu  = _uiDocument.rootVisualElement.Query<VisualElement>("MainMenu");
        _settingsMenu = _uiDocument.rootVisualElement.Query<VisualElement>("SettingsMenu");
        
        _startButton = _mainMenu.Query<Button>("StartGameButton");
        _settingsButton = _mainMenu.Query<Button>("SettingsButton");
        _exitButton = _mainMenu.Query<Button>("ExitButton");
        _startButton.RegisterCallback<ClickEvent>(OnStartClick);
        _settingsButton.RegisterCallback<ClickEvent>(OnSettingsClick);
        _exitButton.RegisterCallback<ClickEvent>(OnExitClick);
        
        _backToMenuButton =  _settingsMenu.Query<Button>("BackToMain");
        _backToMenuButton.RegisterCallback<ClickEvent>(OnBackToMenuClick);
        
        print(_settingsMenu.childCount);
        VisualElement sound = _settingsMenu.Query<VisualElement>("Sound");
        print(sound.childCount);
        
        _masterVolumeSlider = sound.Query<SliderInt>("MasterSlider");
        _musicVolumeSlider  = sound.Query<SliderInt>("MusicSlider");
        _sfxVolumeSlider = sound.Query<SliderInt>("SFXSlider");
        _masterVolumeSlider.RegisterCallback<ChangeEvent<int>>(OnMasterVolumeChange);
        _musicVolumeSlider.RegisterCallback<ChangeEvent<int>>(OnMusicVolumeChange);
        _sfxVolumeSlider.RegisterCallback<ChangeEvent<int>>(OnSfxVolumeChange);
        
        _settingsMenu.style.display = DisplayStyle.None;
        _mainMenu.style.display = DisplayStyle.Flex;
        
        settings.MasterVolume = PlayerPrefs.GetInt("MasterVolume", 80);
        settings.MusicVolume = PlayerPrefs.GetInt("MusicVolume", 80);
        settings.SfxVolume = PlayerPrefs.GetInt("SfxVolume", 80);
        
        _masterVolumeSlider.value = settings.MasterVolume;
        _musicVolumeSlider.value = settings.MusicVolume;
        _sfxVolumeSlider.value = settings.SfxVolume;
        
    }

    void OnMasterVolumeChange(ChangeEvent<int> changeEvent)
    {
        settings.MasterVolume = changeEvent.newValue;
    }

    void OnMusicVolumeChange(ChangeEvent<int> changeEvent)
    {
        settings.MusicVolume = changeEvent.newValue;
    }

    void OnSfxVolumeChange(ChangeEvent<int> changeEvent)
    {
        settings.SfxVolume = changeEvent.newValue;
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
        PlayerPrefs.SetInt("MasterVolume",  settings.MasterVolume);
        print(settings.MasterVolume);
        PlayerPrefs.SetInt("MusicVolume", settings.MusicVolume);
        PlayerPrefs.SetInt("SfxVolume", settings.SfxVolume);
        _mainMenu.style.display = DisplayStyle.Flex;
        _settingsMenu.style.display = DisplayStyle.None;
    }

    void OnExitClick(ClickEvent evt)
    {
        Application.Quit();
    }
}

