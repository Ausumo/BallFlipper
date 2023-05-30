using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private void FixedUpdate()
    {
        transform.Translate(_moveSpeed * Time.deltaTime, 0, 0);
    }
}
