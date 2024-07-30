using UnityEngine;

public class CrudeBillboard : MonoBehaviour
{
    void Update()
    {
        //Face the camera
        transform.forward = GameManager.Camera.transform.forward;
    }
}
