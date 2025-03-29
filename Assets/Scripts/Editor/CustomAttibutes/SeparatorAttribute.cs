using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace SE.EvilLib.Core
{

	/// <summary>
	/// Draw a separator line.
	/// Optional param 1: Title (string, written in the middle)
	/// Optional param 2: rgba color for title (float, float, float, float -- def = yellow)
	/// </summary>
	public class SeparatorAttribute : PropertyAttribute
	{
		public readonly string title;
		public readonly Color textCol = Color.yellow;

		Color debugColor = Color.red;

		public SeparatorAttribute()
		{
			this.title = "";
		}

		public SeparatorAttribute(string _title)
		{
			this.title = _title.ToUpper();

			if (this.title == "DEBUG")
				this.textCol = debugColor;
		}

		public SeparatorAttribute(string _title, int colorR, int colorG, int colorB, int colorA)
		{
			this.title = _title.ToUpper();
			textCol = new Color32((byte)colorR, (byte)colorG, (byte)colorB, (byte)colorA);
		}
	}

	#if UNITY_EDITOR
	

	[CustomPropertyDrawer(typeof(SeparatorAttribute))]
	public class SeparatorDrawer : DecoratorDrawer
	{
		SeparatorAttribute separatorAttribute { get { return ((SeparatorAttribute)attribute); } }

		GUIStyle labelStyle = new GUIStyle();
		Texture2D texBkg = null;

		public override void OnGUI(Rect _position)
		{
			texBkg = new Texture2D(1, 1, TextureFormat.ARGB32, false);
			texBkg.SetPixel(1, 1, new Color(1f, 1f, 1f, .3f));
			texBkg.Apply();

			if (separatorAttribute.title == "")
			{
				_position.height = 1;
				_position.y += 19;
				GUI.DrawTexture(_position, texBkg);
			}
			else
			{
				Vector2 textSize = GUI.skin.label.CalcSize(new GUIContent(separatorAttribute.title));
				float separatorWidth = (_position.width - textSize.x) / 2.0f - 5.0f;
				_position.y += 19;

				labelStyle.fontStyle = FontStyle.Bold;
				labelStyle.normal.textColor = separatorAttribute.textCol;


				GUI.DrawTexture(new Rect(
											_position.xMin,
											_position.y - 2f,
											separatorWidth,
											2
										),
								texBkg);
				GUI.Label(new Rect(
											_position.xMin + separatorWidth + 5.0f,
											_position.yMin - 8.0f,
											textSize.x,
											20
										),
							separatorAttribute.title,
							labelStyle);

				GUI.DrawTexture(new Rect(
											_position.xMin + separatorWidth + 10.0f + textSize.x,
											_position.y - 2f,
											separatorWidth,
											2
										),
								texBkg);
			}
		}


		public override float GetHeight()
		{
			return 30.0f;
		}

	}
	#endif
}
