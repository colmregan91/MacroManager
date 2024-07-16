
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using  UnityEngine.UI;
using Utils;
using System;
public class FoodSelectUI : MonoBehaviour
{
    [SerializeField] private RawImage image;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button button;
    private Food _foodAtThisSelection;
    
    public void Init(Food food, Action<Food> callback)
    {
        _foodAtThisSelection = food;
        button.onClick.AddListener(()=> callback?.Invoke(_foodAtThisSelection));
        Texture2D texture2D = TextureUtils.GetTextureFromData(_foodAtThisSelection.TextureData);
        image.texture =texture2D ;
        image.material.mainTexture = texture2D;
        text.text =  _foodAtThisSelection.name;
    }
    

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }
}
