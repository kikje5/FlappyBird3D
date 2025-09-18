using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
    
public class GameOver : MonoBehaviour
{
    private UIDocument _uiDocument;
    private Button _button;
    private Button _mainMenuButton;
    public Global global;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        _button = _uiDocument.rootVisualElement.Q("Retry") as Button;
        _mainMenuButton =  _uiDocument.rootVisualElement.Q("ReturnMainMenu") as Button;
        
        _button.RegisterCallback<ClickEvent>(OnResetClick);
        _mainMenuButton.RegisterCallback<ClickEvent>(OnReturnMainMenuClick);
    }
        
    void OnResetClick(ClickEvent evt)
    {
        Debug.Log("OnResetClick");
        global.IsDead = false;
        global.resetObstacles = true;
    }

    void OnReturnMainMenuClick(ClickEvent evt)
    {
        SceneManager.LoadScene(0);
    }
}
