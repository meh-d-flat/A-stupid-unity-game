using UnityEngine;

public class EnvironmentCollider : MonoBehaviour
{
    Color thisColor;
    Renderer rendererThis, rendererCollided;

    void Start()
    {
        rendererThis = gameObject.GetComponent<Renderer>();
        thisColor = gameObject.GetComponent<Renderer>().material.color;
    }

    void OnCollisionEnter(Collision collision)
    {
        thisColor = collision.gameObject.GetComponent<Renderer>().material.color;
        rendererCollided = collision.gameObject.GetComponent<Renderer>();
        rendererThis.material.color = rendererCollided.material.color;
    }
}
