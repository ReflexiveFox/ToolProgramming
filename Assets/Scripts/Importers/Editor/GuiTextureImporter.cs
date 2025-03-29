using UnityEngine;
using UnityEditor;
    
class GuiTextureImporter : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        if (!assetPath.StartsWith("Assets/Textures/UI"))
            return;
        TextureImporter textureImporter  = (TextureImporter)assetImporter;
        textureImporter.filterMode = FilterMode.Point;
        textureImporter.textureType = TextureImporterType.Sprite;
    }

    void OnPostprocessTexture(Texture2D texture)
    {
        if (!assetPath.StartsWith("Assets/Textures/UI"))
            return;
        
        if(texture.width != 512 )
            Debug.LogError("Wrong texture size for GUI! Texture width must be 512");
    }
}
