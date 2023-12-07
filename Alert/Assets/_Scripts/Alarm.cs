using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AlarmSystem _alarmSystem;
    [SerializeField] private AudioSource _sound;
    [SerializeField] private float _maxVolume;

    private float _volumeRate;
    private Coroutine _startCoroutine;
    private Coroutine _stopCoroutine;

    private void Start()
    {
        _sound = GetComponent<AudioSource>();

        _sound.volume = 0f;        
        _volumeRate = 1f;
    }

    private void OnEnable()
    {
        _alarmSystem.OnActivated += MakeLoud;
        _alarmSystem.OnDisabled += MakeSilent;
    }

    private void OnDisable()
    {
        _alarmSystem.OnActivated -= MakeLoud;
        _alarmSystem.OnDisabled -= MakeSilent;
    }

    private void MakeLoud()
    {
        if (_stopCoroutine != null)
        {
            StopCoroutine(_stopCoroutine);
        }

        _startCoroutine = StartCoroutine(RegulateSound(_maxVolume));
        _sound.Play();
    }

    private void MakeSilent()
    {
        if (_startCoroutine != null)
        {
            StopCoroutine(_startCoroutine);
        }

        _stopCoroutine = StartCoroutine(RegulateSound(0));
    }

    private IEnumerator RegulateSound(float volume)
    {
        while (_sound.volume != volume)
        {
            _sound.volume = Mathf.Lerp(_sound.volume, volume, _volumeRate * Time.deltaTime);
            yield return null;
        }

        if (_sound.volume == 0)
        {
            _sound.Stop();
        }
    }
}
