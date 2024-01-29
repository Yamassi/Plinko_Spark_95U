using UnityEngine;
using UnityEngine.UI;

public class InAppButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    private void OnValidate()
    {
        _button = GetComponent<Button>();
    }
    private void OnEnable()
    {
        _button.onClick.AddListener(OpenInAppLoadingPage);
    }
    private void OnDisable()
    {
        _button.onClick.RemoveListener(OpenInAppLoadingPage);
    }
    private void OpenInAppLoadingPage()
    {
        InApp.Instance.OpenInAppLoadingPage("Loading");
    }
}
