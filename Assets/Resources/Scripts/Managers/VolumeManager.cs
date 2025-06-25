using UnityEngine;
using UnityEngine.UI;

public class VolumeControlUI : MonoBehaviour
{
    [SerializeField]
    private Slider volumeSlider; // ���� �������� ���� UI Slider �� Inspector

    void Start()
    {
        if (volumeSlider == null)
        {
            Debug.LogError("VolumeControlUI: ������� �� �������� � ����������, ���!");
            return;
        }

        // ����������, ��� ��� ������ ����������� �������� ����������
        if (BackgroundMusicManager.Instance == null)
        {
            Debug.LogError("VolumeControlUI: BackgroundMusicManager �� ������ �� �����! �������, ��� �� ���������� � ���������� DontDestroyOnLoad.");
            return;
        }

        // ������������� ��������� �������� �������� �� ������� ��������� ������� �����
        volumeSlider.value = BackgroundMusicManager.Instance.GetVolume();

        // ��������� ��������� � ��������
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    void OnVolumeChanged(float newVolume)
    {
        // ����� �� �������� ����� SetVolume ������ BackgroundMusicManager
        if (BackgroundMusicManager.Instance != null)
        {
            BackgroundMusicManager.Instance.SetVolume(newVolume);
        }
    }

    void OnDestroy()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
        }
    }
}


//using UnityEngine;
//using UnityEngine.UI;

//public class VolumeControl : MonoBehaviour
//{
//    [SerializeField]
//    private AudioSource audioSource;

//    [SerializeField]
//    private Slider volumeSlider;

//    void Start()
//    {
//        if (audioSource == null)
//        {
//            audioSource = GetComponent<AudioSource>();
//            if (audioSource == null)
//            {
//                return;
//            }
//        }

//        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

//        volumeSlider.value = audioSource.volume;
//    }

//    void OnVolumeChanged(float newVolume)
//    {
//        audioSource.volume = newVolume;
//    }

//    void OnDestroy()
//    {
//        if (volumeSlider != null)
//        {
//            volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
//        }
//    }
//}