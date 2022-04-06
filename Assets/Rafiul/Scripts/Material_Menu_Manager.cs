using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Material_Menu_Manager : MonoBehaviour
{
    public RectTransform menuRoot;
    public RectTransform matMenuRoot;
    public RectTransform contentPanelRoot;
    public GameObject shirtMaterial;
    public List<Color> availableColors;
    public GameObject colorButtonPrefab;
    public List<Texture2D> clothTexture;
    public List<Texture2D> leatherTexture;
    public GameObject texturePrefab;
    public RectTransform texturePanelRoot;

    private bool isOpen;
    private bool matOpen;



    void Awake()
    {
        isOpen = false;
        matOpen = false;
        menuRoot.anchoredPosition = new Vector2(800f, menuRoot.anchoredPosition.y);
        matMenuRoot.anchoredPosition = new Vector2(-800f, matMenuRoot.anchoredPosition.y);
        AssignFunctionalityOnColor();
    }

    void Start()
    {
        ChangeMaterialValue(0);
    }

    public void ToggleMenu()
    {
        isOpen = !isOpen;
        float positionXToMove = isOpen ? 60f : 800f;
        menuRoot.DOAnchorPosX(positionXToMove, 1f);
    }

    public void ToggleMatMenu()
    {
        matOpen = !matOpen;
        float positionXToMove = matOpen ? -40f : -800f;
        matMenuRoot.DOAnchorPosX(positionXToMove, 1f);
    }

    public void AssignFunctionalityOnColor()
    {
        foreach (Color col in availableColors)
        {
            GameObject g = Instantiate(colorButtonPrefab, contentPanelRoot);
            g.GetComponent<Image>().color = col;
            g.GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log("Changing Mat");
                var shirtRenderer = shirtMaterial.GetComponent<Renderer>();
                shirtRenderer.material.SetColor("_BaseColor", col);
            });
        }
    }

    public void ClearTextureList()
    {
        foreach (Transform child in texturePanelRoot)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void ChangeMaterialValue(int id)
    {
        // id -> 
        // 0 for Regular Cloth
        // 1 for Leather
        // 2 for Silk
        // 3 for Rubber

        var shirtRenderer = shirtMaterial.GetComponent<Renderer>();
        shirtRenderer.material.SetTexture("_BaseMap", null);
        float smoothness = 0f;
        float metallic = 0f;
        ClearTextureList();

        if (id == 0)
        {
            smoothness = 0.205f;
            metallic = 0f;
            ShowTexture(id);
        }
        else if (id == 1)
        {
            smoothness = 0.5f;
            metallic = 0f;
            ShowTexture(id);
        }
        else if (id == 2)
        {
            smoothness = 0.5f;
            metallic = 1f;
        }
        else if (id == 3)
        {
            smoothness = 0.15f;
            metallic = 0.36f;
        }

        shirtRenderer.material.SetFloat("_Smoothness", smoothness);
        shirtRenderer.material.SetFloat("_Metallic", metallic);

    }

    public void ShowTexture(int Id)
    {
        if (Id == 0)
        {
            foreach (Texture2D tex in clothTexture)
            {
                Sprite s = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f),
                    100.0f);
                GameObject g = Instantiate(texturePrefab, texturePanelRoot);
                g.GetComponent<Image>().sprite = s;
                g.GetComponent<Button>().onClick.AddListener(() =>
                {
                    var shirtRenderer = shirtMaterial.GetComponent<Renderer>();
                    shirtRenderer.material.SetTexture("_BaseMap",tex);
                });
            }
        }
        else if (Id == 1)
        {
            foreach (Texture2D tex in leatherTexture)
            {
                Sprite s = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f),
                    100.0f);
                GameObject g = Instantiate(texturePrefab, texturePanelRoot);
                g.GetComponent<Image>().sprite = s;
                g.GetComponent<Button>().onClick.AddListener(() =>
                {
                    var shirtRenderer = shirtMaterial.GetComponent<Renderer>();
                    shirtRenderer.material.SetTexture("_BaseMap", tex);
                });
            }
        }
        
    }
}
