﻿using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

public class Locator
{
	static Dictionary<Type, object> objects = new Dictionary<Type, object>();

	public static void Clear()
	{
		objects.Clear();
	}

	public static T Get<T>()
	{
		object obj;

		objects.TryGetValue(typeof(T), out obj);

		if(obj == null)
		{
			if(typeof(Object).IsAssignableFrom(typeof(T)))
			{
				obj = Object.FindObjectOfType(typeof(T));
			}
			else
			{
				obj = Activator.CreateInstance<T>();
			}
			objects[typeof(T)] = obj;
		}

		return (T) obj;
	}
}