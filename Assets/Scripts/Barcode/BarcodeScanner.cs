using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ZXing;
using ZXing.Common;

public class BarcodeScanner : MonoBehaviour
{
    private WebCamTexture camTexture;
    private IBarcodeReader barcodeReader;
   [SerializeField] private RectTransform parentRect;
   public Vector2 offset;
    void Start()
    {
        
        barcodeReader = new BarcodeReader
        {
            AutoRotate = true,

            Options = new DecodingOptions
            {
                PossibleFormats = new List<BarcodeFormat> 
                {   
                    BarcodeFormat.AZTEC,
                    BarcodeFormat.CODABAR,
                    BarcodeFormat.CODE_39,
                    BarcodeFormat.CODE_93,
                    BarcodeFormat.CODE_128,
                    BarcodeFormat.DATA_MATRIX,
                    BarcodeFormat.EAN_8,
                    BarcodeFormat.EAN_13,
                    BarcodeFormat.ITF,
                    BarcodeFormat.MAXICODE,
                    BarcodeFormat.PDF_417,
                    BarcodeFormat.QR_CODE,
                    BarcodeFormat.RSS_14,
                    BarcodeFormat.RSS_EXPANDED,
                    BarcodeFormat.UPC_A,
                    BarcodeFormat.UPC_E,
                    BarcodeFormat.UPC_EAN_EXTENSION}, 
                TryHarder = true,
            }
        };
        
        
        camTexture = new WebCamTexture();
        camTexture.requestedHeight = Screen.height;
        camTexture.requestedWidth = Screen.width;
     

    }

    public void StartScan()
    {
        camTexture.Play();

    }
    public void EndScan()
    {
        camTexture.Stop();
    }
    void OnGUI()
    {
        if (camTexture.isPlaying)
        {
            GUI.DrawTexture(new Rect(parentRect.rect.position + offset, new Vector2(parentRect.rect.width, parentRect.rect.height)), camTexture, ScaleMode.ScaleToFit);

            try
            {
                var result = barcodeReader.Decode(camTexture.GetPixels32(), camTexture.width, camTexture.height);
                if (result != null)
                {
                    Debug.Log("Decoded from camera: " + result.Text);
             
  
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning(ex.Message);
            }
        }

    }
}