/*
 * Copyright 2012 ZXing.Net authors
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class BarcodeCam : MonoBehaviour
{

       
    // Texture for encoding test
    public Texture2D encoded;

    private WebCamTexture camTexture;
    private Thread qrThread;

    private Color32[] c;
    private int W, H;
    
    public RawImage rawImage;
    private bool isQuit;

    public string LastResult;
    private bool shouldEncodeNow;

    public Transform focusPoint;

    public Action<string> OnBarCodeScannned;
    public Action<Texture2D> OnTextureCaptured;
    public void Init()
    {
        encoded = new Texture2D(256, 256);
        LastResult = "http://www.google.com";
        shouldEncodeNow = true;
        
        camTexture = new WebCamTexture();
        rawImage.texture = camTexture;
        rawImage.material.mainTexture = camTexture;

        qrThread = new Thread(Decode);
        qrThread.Start();
        
        if (camTexture != null)
        {
            camTexture.Play();
            W = camTexture.width;
            H = camTexture.height;
            float halfW = W / 2;
            float halfY = H / 2;
            camTexture.autoFocusPoint = focusPoint.position;
        }
    }

    public  void Deinit()
    {
        if (qrThread != null)
        {
            qrThread.Abort();
        }

        if (camTexture != null)
        {
       
            camTexture.Stop();
            camTexture = null;
        }
        
    }
    

    // It's better to stop the thread by itself rather than abort it.
    void OnApplicationQuit()
    {
        isQuit = true;
    }

    void Update()
    {


        if (c == null)
        {
            c = camTexture.GetPixels32();
        }

        // encode the last found
        var textForEncoding = LastResult;
        if (shouldEncodeNow &&
            textForEncoding != null)
        {
            var color32 = Encode(textForEncoding, encoded.width, encoded.height);
            encoded.SetPixels32(color32);
            encoded.Apply();
            shouldEncodeNow = false;
            
        }

    }


    public void Test()
    {
        OnBarCodeScannned?.Invoke(5201054017418.ToString());
    }

    public void CaptureImage()
    {
        Texture2D capturedTexture = new Texture2D(camTexture.width, camTexture.height);
        capturedTexture.SetPixels(camTexture.GetPixels());
        capturedTexture.Apply();
        OnTextureCaptured?.Invoke(capturedTexture);
    }


    void Decode()
    {
        // create a reader with a custom luminance source
        var barcodeReader = new BarcodeReader { AutoRotate = false, Options = new ZXing.Common.DecodingOptions { TryHarder = true } };

        while (true)
        {
            if (isQuit)
                break;

            try
            {
                // decode the current frame
                var result = barcodeReader.Decode(c, W, H);
                if (result != null)
                {
                    LastResult = result.Text;
                    shouldEncodeNow = true;
                    OnBarCodeScannned?.Invoke(LastResult);
                  
                }

                // Sleep a little bit and set the signal to get the next frame
                Thread.Sleep(200);
                c = null;
            }
            catch
            {
            }
        }
    }

    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }
}