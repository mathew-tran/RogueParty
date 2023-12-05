using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageComponentUI : MonoBehaviour
{
    public DamageComponent damageComponentReference;
    public Text textComponent;
    public Slider sliderComponent;
    // Start is called before the first frame update
    
    public void UpdateUI()
    {
        textComponent.text = damageComponentReference.GetHealth().ToString() + "/" + damageComponentReference.GetMaxHealth().ToString();
        sliderComponent.value =  (float)damageComponentReference.GetHealth() / (float)damageComponentReference.GetMaxHealth();
    }
    void Start()
    {
        damageComponentReference.onHealthChange += UpdateUI;
        UpdateUI();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
