using UnityEngine;

public class DetectableObject : MonoBehaviour, IDetectable
{
    [Header("Data")]
    [SerializeField] SO_Detectable _data;
    [SerializeField] DetectableObjectUI _ui;

    public SO_Detectable Data => _data;

    private void Reset()
    {
        _ui = GetComponent<DetectableObjectUI>();
    }

    void Start()
    {
        _ui.Init(Data);
    }

    public void Detected()
        => StartCoroutine(_ui.DisplayPopupForSeconds());
}