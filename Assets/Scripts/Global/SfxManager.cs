using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager instance;

    [Header("Sources")]
    [SerializeField] private AudioSource _sfx;
    [SerializeField] private AudioSource _ui;

    [Header("Player")]
    [SerializeField] private AudioClip _playerShoot;
    [SerializeField] private AudioClip _playerSecondShoot;
    [SerializeField] private AudioClip _playerDamaged;
    [SerializeField] private AudioClip _playerDie;

    [Header("Enemies")]
    [SerializeField] private AudioClip _enemyShoot;
    [SerializeField] private AudioClip _enemyDamaged;
    [SerializeField] private AudioClip _enemyDie;

    [Header("UI")]
    [SerializeField] private AudioClip _btnHover;
    [SerializeField] private AudioClip _btnClick;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {

        UiButtonHoverSFXEvent.OnButtonHover += OnButtonHover_PlayClip;
        UiButtonHoverSFXEvent.OnButtonClick += OnButtonClick_PlayClip;
    }

    private void OnDisable()
    {
        
        UiButtonHoverSFXEvent.OnButtonHover -= OnButtonHover_PlayClip;
        UiButtonHoverSFXEvent.OnButtonClick -= OnButtonClick_PlayClip;
    }

    private void OnButtonHover_PlayClip()
    {
        _ui.PlayOneShot(_btnHover);
    }

    private void OnButtonClick_PlayClip()
    {
        _ui.PlayOneShot(_btnClick);
    }
}
