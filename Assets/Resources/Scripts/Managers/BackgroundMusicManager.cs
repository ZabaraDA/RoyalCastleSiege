using UnityEngine;
using UnityEngine.UI; // ���� ������, ����� ������ ���� ��� "������"

public class BackgroundMusicManager : MonoBehaviour
{
    // ����������� ����������, ����� ���������, ��� � ��� ������ ���� �������� ������
    public static BackgroundMusicManager Instance;

    // ������ �� AudioSource � Slider (���� ����� �� ���� ��������� �� ����� �������)
    private AudioSource audioSource;

    void Awake()
    {
        // ����� �����: ���������, ���������� �� ��� ��� "������" ��������.
        // ���� ��, �� ���������� ���� �����, ����� �� ���� ������������ �����.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return; // ������� �� ������, ����� �� ���������� �������������
        }

        // ���� ��� ������ � ������������ ��������, �� �� ���������� "Instance"
        Instance = this;

        // ������� �����: "�� ������ ���� ��� �������� ����� ����!"
        DontDestroyOnLoad(gameObject);

        // �������� AudioSource �� ���� �� �������
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("���, �� ������� BackgroundMusicManager ��� AudioSource!");
        }

        // �����������: ���� �� ������, ����� ������� ���� ��� "������" � �������� ���� ������,
        // �� ���� ����� ����� ����� ��� ����� ��� �������� ������.
        // ����� �����, ���� �� ���� ��������� �� ������� � DontDestroyOnLoad.
        // �����, ���� ����� ����� ������� ��� ����� �����-�� ������ ��������.
        // ���� ���, ����� �������, ��� ������� ����� ���� � �� ������� �����.
    }

    // ����� ��� ��������� ���������, ������� ����� ������� �� ������ ��������
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }

    // ����� ��� ��������� ������� ���������
    public float GetVolume()
    {
        if (audioSource != null)
        {
            return audioSource.volume;
        }
        return 0f; // ���������� 0, ���� AudioSource �� ������
    }

    // ���� ������ ��������� ������/����������������
    public void PlayMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void PauseMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }
}


