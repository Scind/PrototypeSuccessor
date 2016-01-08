using UnityEngine;
using System.Collections;

#region under construction 

public static class CustomCursor
{
    static Vector3 Offset;
    static Vector3 position = Vector3.zero;
    static Rect drawRect;
    public static Rect DrawRect { get { return drawRect; }}
    public static Vector3 Position { get { return position; } set
        {
            position = value;
            drawRect.position = value + Offset;
        } }

    public static Texture2D Texture { get; private set; }

    public static void Initialize(Texture2D texture, Vector3 Offset)
    {
        drawRect.width = texture.width;
        drawRect.height = texture.height;
        Position = new Vector3(Screen.width/2, Screen.height/2,0);
        CustomCursor.Offset = Offset;
        Texture = texture;
    }
}

#endregion

public class GUIRenderer : MonoBehaviour {

    public Texture2D CustomCursorTexture;

    void Awake()
    {
        Cursor.visible = false;
        CustomCursor.Initialize(CustomCursorTexture, new Vector3(CustomCursorTexture.width, 0));
    }

    void Update()
    {
        CustomCursor.Position += new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
    }

    void OnGUI()
    {
        GUI.DrawTexture(CustomCursor.DrawRect, CustomCursor.Texture);
    }
}
