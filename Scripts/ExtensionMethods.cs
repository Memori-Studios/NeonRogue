using UnityEngine;
using System.Collections;

//It is common to create a class to contain all of your
//extension methods. This class must be static.
public static class ExtensionMethods
{
    //Even though they are used like normal methods, extension
    //methods must be declared static. Notice that the first
    //parameter has the 'this' keyword followed by a Transform
    //variable. This variable denotes which class the extension
    //method becomes a part of.
    public static void ResetTransformation(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1, 1, 1);
    }
    public static IEnumerator FadeIn(this CanvasGroup canvasGroup, float duration = 0.5f)
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
        float startTime = Time.time;

        float t = 0.0f;
        while (canvasGroup.alpha < 1)
        {
            t = (Time.time-startTime) / duration;
            canvasGroup.alpha = t;
            yield return null;
        }
        
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }
    public static IEnumerator FadeOut(this CanvasGroup canvasGroup, float duration = 0.5f)
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        float startTime = Time.time;

        float t = 0.0f;
        while (canvasGroup.alpha > 0)
        {
            t = (Time.time-startTime) / duration;
            canvasGroup.alpha = 1 - t;
            yield return null;
        }
        
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }
    public static void Enable(this CanvasGroup canvasGroup)
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }
    public static void Disable(this CanvasGroup canvasGroup)
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }
    public static T GetClosestObject<T>(Vector3 currentPosition, T[] objects, T objectToAvoid = null) where T : Component
    {
        T closestT = null;
        float minDist = Mathf.Infinity;
        foreach (T obj in objects)
        {
            if(obj==objectToAvoid)
                continue;

            float dist = Vector3.Distance(obj.gameObject.transform.position, currentPosition);
            
            if (dist < minDist)
            {
                closestT = obj;
                minDist = dist;
            }
        }
        return closestT;
    }
}