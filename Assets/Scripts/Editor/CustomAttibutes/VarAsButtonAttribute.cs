using UnityEngine;
using System;
using System.Reflection;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SE.EvilLib.Core
{

	/// <summary>
	/// Display bool variable as button, calling a function on click.
	/// Param 1: the public method name to call on click.
	/// Optional param 2: button label (default = bool var name).
	/// Optional param 3: ability to execute in edit mode (default = false)
	/// </summary>
	public class VarAsButtonAttribute : PropertyAttribute
	{
		public readonly string methodName;
		public readonly string buttonTitle;
		public readonly bool executeInEditMode;

		public VarAsButtonAttribute(string _methodName)
		{
			this.methodName = _methodName;
			this.buttonTitle = "";
			this.executeInEditMode = false;
		}

		public VarAsButtonAttribute(string _methodName, string _buttonTitle)
		{
			this.methodName = _methodName;
			this.buttonTitle = _buttonTitle;
			this.executeInEditMode = false;
		}

		public VarAsButtonAttribute(string _methodName, string _buttonTitle, bool _execInEditMode)
		{
			this.methodName = _methodName;
			this.buttonTitle = _buttonTitle;
			this.executeInEditMode = _execInEditMode;
		}
	}

	#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(VarAsButtonAttribute))]
	public class VarAsButtonDrawer : PropertyDrawer
	{
		VarAsButtonAttribute boolAsButtonAttribute { get { return ((VarAsButtonAttribute)attribute); } }

		string buttonLabel = "BUTTON";
		GUIStyle buttonStyle = new GUIStyle(EditorStyles.miniButtonMid);

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			Type type = property.serializedObject.targetObject.GetType();
			MethodInfo methodToCall = type.GetMethod(boolAsButtonAttribute.methodName);

			if (boolAsButtonAttribute.buttonTitle == "")
				buttonLabel = property.displayName;
			else
				buttonLabel = boolAsButtonAttribute.buttonTitle;

			if (!boolAsButtonAttribute.executeInEditMode && !Application.isPlaying)
				GUI.enabled = false;

			if (GUI.Button(position, buttonLabel, buttonStyle))
			{
				foreach (UnityEngine.Object targetObject in property.serializedObject.targetObjects)
					methodToCall.Invoke(targetObject, null);
			}
			GUI.enabled = true;
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return 20f;
		}

	}
	#endif
}
