using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator _animator; public Animator Animator => _animator;
    Vector3 previousPosition = Vector3.zero;
    Vector3 velocity = Vector3.zero;
    void Update()
    {
        if (velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.LookRotation(new Vector3(velocity.normalized.x, 0, velocity.normalized.z)), 0.1f);
        }
        _animator.SetFloat("velocity", velocity.magnitude);
        velocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;

    }
}
