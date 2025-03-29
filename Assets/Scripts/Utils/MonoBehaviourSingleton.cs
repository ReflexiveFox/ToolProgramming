using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T _instance;
	public static T Instance => CreateSingleton();
	

	private static T CreateSingleton( )
	{ 
		if( _instance == null )
		{
			var objs = FindObjectsOfType<T>();
            if (objs.Length > 0)
			{
				_instance = objs[0];
				return _instance;
			}

            GameObject obj = new GameObject( );
			obj.hideFlags = HideFlags.DontSave;
			_instance = obj.AddComponent<T>();
			return _instance;
		}
		return _instance;
	}
}
