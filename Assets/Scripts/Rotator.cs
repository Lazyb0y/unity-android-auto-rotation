using UnityEngine;

public class Rotator : MonoBehaviour
{
    void Start()
    {
        var cubeRigidbody = GetComponent<Rigidbody>();
        cubeRigidbody.angularVelocity = Random.insideUnitSphere;
    }
}
