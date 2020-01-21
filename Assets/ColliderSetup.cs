using UnityEngine;

public class ColliderSetup : MonoBehaviour
{
    bool collides;

	public bool IsColliding {
		get { return collides; }
	}
    void OnCollisionEnter(Collision collision) {
        collides = ChangeStatus(collision);
    }
    void OnCollisionExit(Collision collision) {
        collides = ChangeStatus(collision);
    }
    void OnCollisionStay(Collision collision) {
        collides = ChangeStatus(collision);
    }
    bool ChangeStatus(Collision collision) {
        return collision.contacts.Length > 0;
    }
}
