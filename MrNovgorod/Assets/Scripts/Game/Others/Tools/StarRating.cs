using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HorizontalLayoutGroup))]
public class StarRating : MonoBehaviour
{
    [SerializeField] private Image starPrefab;
    [SerializeField] private int maxRating = 5;
    [SerializeField] private Sprite fullStarSprite;
    [SerializeField] private Sprite emptyStarSprite;
    
    private List<Image> stars = new List<Image>();
    private float currentRating;

    private void Awake()
    {
        InitializeStars();
        SetRating(0);
    }

    private void InitializeStars()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        stars.Clear();

        for (int i = 0; i < maxRating; i++)
        {
            Image star = Instantiate(starPrefab, transform);
            stars.Add(star);
            
            Button button = star.gameObject.AddComponent<Button>();
            int index = i;
            button.onClick.AddListener(() => SetRating(index + 1));
        }
    }

    public void SetRating(float rating)
    {
        currentRating = Mathf.Clamp(rating, 0, maxRating);
        
        for (int i = 0; i < stars.Count; i++)
        {
            if (i < (int)currentRating)
            {
                stars[i].sprite = fullStarSprite;
            }
            else if (i == (int)currentRating && currentRating % 1 > 0)
            {
                stars[i].sprite = GetHalfStarSprite();
            }
            else
            {
                stars[i].sprite = emptyStarSprite;
            }
        }
    }

    private Sprite GetHalfStarSprite()
    {
        return fullStarSprite;
    }

    public float GetRating()
    {
        return currentRating;
    }
}