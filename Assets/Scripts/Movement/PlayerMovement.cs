using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Range(2f, 8f)]
    public float _moveSpeed = 5f;

    public Vector3 _cursorPosition;
    Vector3 _cameraBounds;
    Transform _player;    

    private void Awake()
    {
        _player = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _cursorPosition = (Input.mousePosition / 72f) - (Vector3.one * 5);
        MovePlayer();
    }

    void MovePlayer()
    {
        if (Vector3.Distance(_player.position, _cursorPosition) > 0)
        {
            //Debug.Log(_player.position);
            _player.position =  Vector2.MoveTowards(_player.position, _cursorPosition, _moveSpeed * Time.deltaTime);


            _cameraBounds = Camera.main.WorldToViewportPoint(_player.position);
            _cameraBounds.x = Mathf.Clamp(_cameraBounds.x, 0.024f, 0.976f);
            _cameraBounds.y = Mathf.Clamp(_cameraBounds.y, 0.024f, 0.976f);
            _cameraBounds.z = 10;
            _player.position = Camera.main.ViewportToWorldPoint(_cameraBounds);
        }
    }
}
