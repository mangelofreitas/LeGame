using UnityEngine;
using System.Collections;

public class DestroyCircleByBoundery : MonoBehaviour
{
    public GameController gameController;
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            return;
        }
        gameController.removeCircle(other.gameObject);
        Destroy(other.gameObject);
    }
}
