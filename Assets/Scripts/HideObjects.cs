using UnityEngine;

public class HideObjects : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        other.gameObject.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        other.gameObject.SetActive(true);
    }
}
