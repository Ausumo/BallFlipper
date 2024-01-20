using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRotation : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, _speed * Time.deltaTime);
    }

    public void ChangeDirection()
    {
        _speed = -_speed;
    }
}
