
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
    [SerializeField] private Color selectColor;
    [SerializeField] private Color DeselectColor;
    private Food _foodAtThisSelection;

    public Food FoodAtThisSelection => _foodAtThisSelection;
    
    public void Init(Food food, Action<FoodSelectUI> callback)
    {
        _foodAtThisSelection = food;
        button.onClick.AddListener(()=> callback?.Invoke(this));
        Texture2D texture2D = TextureUtils.GetTextureFromData(_foodAtThisSelection.TextureData);
        image.texture =texture2D ;
        image.material.mainTexture = texture2D;
        text.text =  _foodAtThisSelection.name;
    }
    
    public void OnSelect()
    {
        button.image.color = selectColor;
    }
    public void OnDeselect()
    {
        button.image.color = DeselectColor;
    }
    

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }
}
