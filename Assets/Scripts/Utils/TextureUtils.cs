using UnityEngine;

namespace Utils
{
    public class TextureUtils : MonoBehaviour
    {

        public static Texture2D  GetTextureFromData(TextureData data)
        {
            Texture2D texture = new Texture2D(data.width, data.height, data.textureFormat, false);
            texture.LoadRawTextureData(data.base64Data);
            texture.Apply();
            return texture;
        }
        
        public static TextureData  GetDataFromTexture(Texture2D data)
        {

            TextureData newTexture = new TextureData() { base64Data = data.GetRawTextureData(), width = data.width, height = data.height, textureFormat = data.format };
            
            return newTexture;
        }
        
  
    }
}
