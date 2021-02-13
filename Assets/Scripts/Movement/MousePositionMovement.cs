using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionMovement : MonoBehaviour
{
    public Vector3 _cursorPosition;
    Vector3 _cameraBounds;

    // Update is called once per frame
    void Update()
    {

        //use this to move crosshair
        if (Time.timeScale > 0)
        {
            Cursor.visible = false;
            _cursorPosition = Input.mousePosition;
            _cursorPosition = Camera.main.ScreenToWorldPoint(_cursorPosition);
            transform.position = Vector2.Lerp(transform.position, _cursorPosition, 5f);

            _cameraBounds = Camera.main.WorldToViewportPoint(this.transform.position);
            _cameraBounds.x = Mathf.Clamp(_cameraBounds.x, 0.024f, 0.976f);
            _cameraBounds.y = Mathf.Clamp(_cameraBounds.y, 0.024f, 0.976f);
            _cameraBounds.z = 11;
            this.transform.position = Camera.main.ViewportToWorldPoint(_cameraBounds);
        }
    }
}
