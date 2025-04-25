using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
	private static T m_Instance = null;

	public static T Instance
	{
		get
		{
			if (m_Instance == null)
			{
				m_Instance = GameObject.FindObjectOfType(typeof(T)) as T;
				if (m_Instance == null)
				{
					GameObject go = new GameObject(typeof(T).ToString());
					m_Instance = go.AddComponent<T>();
					m_Instance.Init();
				}

			}
			return m_Instance;
		}
	}

	public virtual void Init() { }


	private void OnApplicationQuit()
	{
		m_Instance = null;
	}
	
}
