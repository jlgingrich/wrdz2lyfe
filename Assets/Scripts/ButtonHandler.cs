using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ButtonHandler : MonoBehaviour
{
    private VisualElement frame;
    private Button button;
    public int sceneID;

    void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        var rootVisualElement = uiDocument.rootVisualElement;

        frame = rootVisualElement.Q<VisualElement>("Frame");
        button = frame.Q<Button>("Button");

        button.RegisterCallback<ClickEvent>(ev => MoveToScene());
    }

    void MoveToScene()
    {
        SceneManager.LoadScene(sceneID);
    }
}