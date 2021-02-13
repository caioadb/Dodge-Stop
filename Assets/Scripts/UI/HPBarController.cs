using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{
    [SerializeField]
    HPValue hp = null;
    [SerializeField]
    Image[] _filledHearts = null;
    [SerializeField]
    Sprite fill = null;
    [SerializeField]
    Sprite empty = null;

    int oldValue;


    private void Start()
    {
        oldValue = hp._RuntimeValue;
        hp.OnVariableChange += VariableChangeHandler;
    }

    private void VariableChangeHandler(int newVal)
    {
        if (newVal > oldValue + 1)
        {
            _filledHearts[0].sprite = fill;
            _filledHearts[1].sprite = fill;
            _filledHearts[2].sprite = fill;
        }
        else if (newVal > oldValue)  // if player gets a lifes
        {
            _filledHearts[newVal].sprite = fill;
        }
        else if (newVal == oldValue - 1)   // if player takes damage
        {
            _filledHearts[newVal].sprite = empty;
        }
        oldValue = newVal;
    }
}
