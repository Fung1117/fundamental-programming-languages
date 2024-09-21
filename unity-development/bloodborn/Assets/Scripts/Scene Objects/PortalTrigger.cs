using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    public PortalTrigger targetPortal;
    static public bool isTeleporting = false;
    private IEnumerator coroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTeleporting)
        {   
            isTeleporting = true;
            Vector3 newPosition = targetPortal.transform.position;
            other.transform.position = newPosition;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isTeleporting)
        {
            coroutine = ResetTeleportingFlag(0.5f);
            StartCoroutine(coroutine);
        }
    }

    IEnumerator ResetTeleportingFlag(float delay)
    {
        yield return new WaitForSeconds(delay);
        isTeleporting = false;
    }
}