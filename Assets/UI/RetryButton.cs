using UnityEngine;
using UnityEngine.UIElements;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private UIDocument _uiDocument;
    private Button _button;
    public  Global global;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        _button = _uiDocument.rootVisualElement.Q("Retry") as Button;
        print("button name:" + _button.name);
        _button.RegisterCallback<ClickEvent>(OnResetClick);
    }
        
    void OnResetClick(ClickEvent evt)
    {
        Debug.Log("OnResetClick");
        global.isDead = false;
        global.resetBird = true;
        global.resetObstacles = true;
    }
}
