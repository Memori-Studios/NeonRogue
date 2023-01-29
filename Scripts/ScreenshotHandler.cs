using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum ImageFilterMode {
    Nearest = 0,
    Bilinear = 1,
    Average = 2
}
public class ScreenshotHandler : MonoBehaviour
{
    private ScreenshotHandler instance;
    public Camera myCamera;
    private bool takeScreenshotOnNextFrame;
    public bool transparentBackground;
    public string filepath = "Assets/";
    RenderTexture renderTexture;
    private void Awake()
    {
        instance = this;
        myCamera.backgroundColor = Color.black;
    }
    public IEnumerator onPostRender(string fileName)
    {
        yield return new WaitForEndOfFrame();

        if(takeScreenshotOnNextFrame)
        {
            takeScreenshotOnNextFrame = false;
            renderTexture= myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);

            //used to be 0,0 for bottom left corner, moving it to get center of screen, should have probably just made the screen size 512 by 512
            Rect rect = new Rect((512-renderTexture.width)/2, (512-renderTexture.height)/2, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);
            
            if(transparentBackground)
                renderResult = RemoveBackground(renderResult);

            renderResult = ResizeTexture(renderResult, ImageFilterMode.Average, renderTexture.width, renderTexture.height);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(filepath + fileName +".png", byteArray);
            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
            Debug.Log(filepath + fileName + ".png created");
        }
    }
    private Texture2D RemoveBackground(Texture2D renderResult)
    {
        Color[] pixels = renderResult.GetPixels(0, 0, renderTexture.width, renderTexture.height);
        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i] == Color.black) 
            {
                pixels[i] = Color.clear;
            }
        }
        renderResult.SetPixels(0, 0, renderTexture.width, renderTexture.height, pixels);
        renderResult.Apply();

        return renderResult;
    }
    public void TakeScreenshot(int width, int height, string fileName)
    {
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 32);
        takeScreenshotOnNextFrame = true;
        StartCoroutine(onPostRender(fileName));
    }
    public Texture2D ResizeTexture(Texture2D originalTexture, ImageFilterMode filterMode, int newWidth, int newHeight) 
    {
        //*** Get All the source pixels
        Color[] sourceColor = originalTexture.GetPixels(0);
        Vector2 sourceSize = new Vector2(originalTexture.width, originalTexture.height);

        //*** Calculate New Size
        float textureWidth = newWidth;
        float textureHeight = newHeight;

        //*** Make New
        Texture2D newTexture = new Texture2D((int)textureWidth, (int)textureHeight, TextureFormat.RGBA32, false);

        //*** Make destination array
        Color[] aColor = new Color[(int)textureWidth * (int)textureHeight];

        Vector2 pixelSize = new Vector2(sourceSize.x / textureWidth, sourceSize.y / textureHeight);

        //*** Loop through destination pixels and process
        Vector2 center = new Vector2();
        for (int i = 0; i < aColor.Length; i++) {

            //*** Figure out x&y
            float x = (float)i % textureWidth;
            float y = Mathf.Floor((float)i / textureWidth);

            //*** Calculate Center
            center.x = (x / textureWidth) * sourceSize.x;
            center.y = (y / textureHeight) * sourceSize.y;

            //*** Do Based on mode
            //*** Nearest neighbour (testing)
            if (filterMode == ImageFilterMode.Nearest) {

                //*** Nearest neighbour (testing)
                center.x = Mathf.Round(center.x);
                center.y = Mathf.Round(center.y);

                //*** Calculate source index
                int sourceIndex = (int)((center.y * sourceSize.x) + center.x);

                //*** Copy Pixel
                aColor[i] = sourceColor[sourceIndex];
            }

            //*** Bilinear
            else if (filterMode == ImageFilterMode.Bilinear) {

                //*** Get Ratios
                float ratioX = center.x - Mathf.Floor(center.x);
                float ratioY = center.y - Mathf.Floor(center.y);

                //*** Get Pixel index's
                int indexTL = (int)((Mathf.Floor(center.y) * sourceSize.x) + Mathf.Floor(center.x));
                int indexTR = (int)((Mathf.Floor(center.y) * sourceSize.x) + Mathf.Ceil(center.x));
                int indexBL = (int)((Mathf.Ceil(center.y) * sourceSize.x) + Mathf.Floor(center.x));
                int indexBR = (int)((Mathf.Ceil(center.y) * sourceSize.x) + Mathf.Ceil(center.x));

                //*** Calculate Color
                aColor[i] = Color.Lerp(
                    Color.Lerp(sourceColor[indexTL], sourceColor[indexTR], ratioX),
                    Color.Lerp(sourceColor[indexBL], sourceColor[indexBR], ratioX),
                    ratioY
                );
            }

            //*** Average
            else if (filterMode == ImageFilterMode.Average) {

                //*** Calculate grid around point
                int xFrom = (int)Mathf.Max(Mathf.Floor(center.x - (pixelSize.x * 0.5f)), 0);
                int xTo = (int)Mathf.Min(Mathf.Ceil(center.x + (pixelSize.x * 0.5f)), sourceSize.x);
                int yFrom = (int)Mathf.Max(Mathf.Floor(center.y - (pixelSize.y * 0.5f)), 0);
                int yTo = (int)Mathf.Min(Mathf.Ceil(center.y + (pixelSize.y * 0.5f)), sourceSize.y);

                //*** Loop and accumulate
                Color tempColor = new Color();
                float xGridCount = 0;
                for (int iy = yFrom; iy < yTo; iy++) {
                    for (int ix = xFrom; ix < xTo; ix++) {

                        //*** Get Color
                        tempColor += sourceColor[(int)(((float)iy * sourceSize.x) + ix)];

                        //*** Sum
                        xGridCount++;
                    }
                }

                //*** Average Color
                aColor[i] = tempColor / (float)xGridCount;
            }
        }

        //*** Set Pixels
        newTexture.SetPixels(aColor);
        newTexture.Apply();

        //*** Return
        return newTexture;
}
}