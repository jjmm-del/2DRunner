using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    
    [Header("오디오 소스(Audio Source")]
    [Tooltip("반복 재생될 배경음을 담당하는 컴포넌트")]
    [SerializeField] private AudioSource _bgmSource;
    
    [Tooltip("짧게 재생될 효과음을 담당하는 컴포넌트입니다.")]
    [SerializeField] private AudioSource _sfxSource;
    
    [Header("배경음 클립")]
    [SerializeField]private AudioClip _mainBGM;
    
    [Header("효과음 클립")]
    [SerializeField]private AudioClip[] _jumpSFX;
    [SerializeField]private AudioClip _hitSFX;
    [SerializeField]private AudioClip _gameOverSFX;
    [SerializeField]private AudioClip _clickSFX;

    private bool _isDangerMode;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayMainBGM()
    {
        _isDangerMode = false;
        _bgmSource.clip = _mainBGM;
        _bgmSource.pitch = 1.0f;
        _bgmSource.loop = true;
        _bgmSource.Play();
    }

    public void PlayDangerBGM()
    {
        if (!_isDangerMode)
        {
            _isDangerMode = true;
            _bgmSource.pitch = 1.2f;
        }
    }
    public void StopBGM()
    {
        _bgmSource.Stop();
    }

    public void PlayJump()
    {
        _sfxSource.PlayOneShot(_jumpSFX[Random.Range(0,_jumpSFX.Length)]);
    }

    public void PlayHit()=> _sfxSource.PlayOneShot(_hitSFX);
    public void PlayGameOver()=> _sfxSource.PlayOneShot(_gameOverSFX);
    public void PlayClick()=> _sfxSource.PlayOneShot(_clickSFX);
}
