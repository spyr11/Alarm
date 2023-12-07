using UnityEngine;
using UnityEngine.Events;

public class AlarmSystem : MonoBehaviour
{
    public event UnityAction OnActivated;
    public event UnityAction OnDisabled;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Movement>(out Movement movement))
        {
            OnActivated?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Movement>(out Movement movement))
        {
            OnDisabled?.Invoke();
        }
    }
}
