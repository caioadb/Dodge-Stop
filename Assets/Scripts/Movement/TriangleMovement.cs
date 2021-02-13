using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleMovement : MonoBehaviour
{
    [Range(2f, 7f)]
    public float _speed = 4f;
    [Range(0f, 7f)]
    public float _turnSpeed = 2.2f;

    bool _canMoveTowardPlayer = true;
    Transform _player;

    private void Start()
    {
        StartCoroutine(LeaveField());
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
        if (_canMoveTowardPlayer)
        {
            //rotate towards player
            Vector3 dir = _player.position - transform.position;
            //get the angle from current direction facing to desired target
            float angle = -Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            //set the angle into a quaternion + sprite offset depending on initial sprite facing direction
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            //Roatate current game object to face the target using a slerp function which adds some smoothing to the move
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _turnSpeed * Time.deltaTime);
        }


    }

    IEnumerator LeaveField()
    {
        yield return new WaitForSeconds(2f);
        _canMoveTowardPlayer = false;
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject, 1f);
    }
}
