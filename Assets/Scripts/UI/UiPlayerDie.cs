using UnityEngine;
using UnityEngine.UI;

public class UiPlayerDie : MonoBehaviour
{
    [SerializeField] private Button _btnReplay;
    [SerializeField] private Button _btnMainMenu;
    [SerializeField] private string _sceneReplay;
    [SerializeField] private string _sceneMainMenu;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        //_btnReplay.onClick.AddListener();
        //_btnMainMenu.onClick.AddListener();
    }

    private void OnEnable()
    {
        HealthSystem.OnPlayerDie += OnPlayerDie_ShowText;
    }

    private void OnDisable()
    {
        HealthSystem.OnPlayerDie -= OnPlayerDie_ShowText;
    }


    private void OnPlayerDie_ShowText()
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }
}
