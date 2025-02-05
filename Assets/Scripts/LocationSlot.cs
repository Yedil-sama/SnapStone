using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocationSlot : MonoBehaviour
{
    public Location location;
    public Player owner;
    public int position;
    public Unit unit;
    [SerializeField] private Image sprite;
    [SerializeField] private Image stateImage;
    [SerializeField] private TMP_Text stateText;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color sleepColor;
    [SerializeField] private Color emptyColor;
    [SerializeField] private Color rushColor;
    [SerializeField] private Image attackImage;
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private Image healthImage;
    [SerializeField] private TMP_Text healthText;
    private void Start()
    {
        if (unit == null)
        {
            healthImage.enabled = false;
            healthText.enabled = false;
            attackImage.enabled = false;
            attackText.enabled = false;
        }
        else
        {
            sprite.color = Color.white;
        }
    }
    public void OnClick()
    {
        if (unit == null)
        {
            if (GameManager.Instance.selectedCard == null || GameManager.Instance.selectedCard.gameState != 1 || GameManager.Instance.selectedCard.GetComponent<Unit>() == null) return;
            if (GameManager.Instance.selectedCard.GetComponent<Unit>().cost > owner.mana)
            {
                Debug.Log("Not enough Mana");
                return;
            }
            if (GameManager.Instance.selectedCard.owner != owner)
            {
                if (GameManager.Instance.selectedCard.GetComponent<Spell>() == null)
                {
                    Debug.Log("It's enemy territory");
                    return;
                }
                else
                {

                }
            }
            unit = GameManager.Instance.selectedCard.GetComponent<Unit>();
            owner.mana -= unit.cost;
            Destroy(GameManager.Instance.selectedCard.gameObject);
            healthImage.enabled = true;
            healthText.enabled = true;
            attackImage.enabled = true;
            attackText.enabled = true;
            sprite.color = Color.white;

            if (unit.charge)
            {
                stateImage.color = activeColor;
                unit.state = "ready";
            }
            else if (unit.rush)
            {
                stateImage.color = rushColor;
                unit.state = "rush";
            }
            else
            {
                stateImage.color = sleepColor;
                unit.state = "sleep";
            }


            sprite.sprite = unit.baseStats.sprite;
            healthText.text = unit.health + "";
            attackText.text = unit.attack + "";
        }

        else
        {
            if (GameManager.Instance.selectedUnit == null)
            {
                if (unit.state == "ready")
                {
                    GameManager.Instance.selectedUnit = unit;
                    gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
                }
                
            }
            else
            {
                GameManager.Instance.selectedUnit.Attack(unit);
                GameManager.Instance.selectedUnit = null;
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            
        }        

    }
}
