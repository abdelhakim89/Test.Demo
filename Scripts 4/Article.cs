using UnityEngine;
using System;

[Serializable]
public class Article : MonoBehaviour
{
   public string ArticleName;
   public float Price;
   public int Quantity;
   public Sprite Image;
   [TextArea] public string Description;
}
