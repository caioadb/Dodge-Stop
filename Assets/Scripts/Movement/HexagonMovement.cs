using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonMovement : MonoBehaviour
{
    [Range(4f, 7f)]
    public float _speed = 5f;

    private void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject, 1f);
    }

}
