using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class Utilities
{
    /// <summary>
    /// Fisher-Yates shuffle algorithm. It randomizes index of every element in a list
    /// </summary>
    /// <param name="list"></param>
    /// <typeparam name="T"></typeparam>
    public static void ShuffleRewards<T>(IList<T> list)
    {
        for (var i = list.Count - 1; i > 0; i--)
        {
            var randomIndex = Random.Range(0, i + 1);

            // Swap elements
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }

    /// <summary>
    /// Overload for the situation where two lists' elements are needed to be randomized in the same order
    /// </summary>
    /// <param name="list1"></param>
    /// <param name="list2"></param>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public static void ShuffleRewards<T1, T2>(IList<T1> list1, IList<T2> list2)
    {
        if (list1.Count != list2.Count) throw new ArgumentException("Both lists must have the same count");
        
        for (var i = list1.Count - 1; i > 0; i--)
        {
            var randomIndex = Random.Range(0, i + 1);

            // Swap elements
            (list1[i], list1[randomIndex]) = (list1[randomIndex], list1[i]);
            (list2[i], list2[randomIndex]) = (list2[randomIndex], list2[i]);
        }
    }

    /// <summary>
    /// Changes the given color's alpha to the given value
    /// </summary>
    /// <param name="image">The image whose alpha we want to change></param>
    /// <param name="alpha">Target alpha value</param>
    public static void ChangeAlpha(Image image, float alpha)
    {
        image.color =  new Color(image.color.r, image.color.g, image.color.b, alpha);
    }
    
    /// <summary>
    /// Changes the given color's alpha to the given value
    /// </summary>
    /// <param name="textMeshProUGUI"> The textMeshProUGUI whose alpha we want to change></param>
    /// <param name="alpha">Target alpha value</param>
    public static void ChangeAlpha(TextMeshProUGUI textMeshProUGUI, float alpha)
    {
        textMeshProUGUI.color =  new Color(textMeshProUGUI.color.r, textMeshProUGUI.color.g, textMeshProUGUI.color.b, alpha);
    }
}
