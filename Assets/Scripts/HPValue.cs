using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HPValue : ScriptableObject, ISerializationCallbackReceiver
{

	public delegate void OnVariableChangeDelegate(int newVal);
	public event OnVariableChangeDelegate OnVariableChange;

	public int InitialValue;

	[NonSerialized]
	private int RuntimeValue;

	public int _RuntimeValue
	{
		get { return RuntimeValue; }
		set
		{
			if (RuntimeValue == value) return;
			RuntimeValue = value;
			if (OnVariableChange != null)
				OnVariableChange(RuntimeValue);
		}
	}

	public void OnAfterDeserialize()
	{
		RuntimeValue = InitialValue;
	}

	public void OnBeforeSerialize() { }
}
