using UnityEngine;

public class FrameRateManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("������� ������� ������")]
    private int _targetFrameRate;
    void Start()
    {
        // Limit framerate to cinematic 24fps.
        QualitySettings.vSyncCount = 0; // Set vSyncCount to 0 so that using .targetFrameRate is enabled.
        Application.targetFrameRate = _targetFrameRate;
    }
}
