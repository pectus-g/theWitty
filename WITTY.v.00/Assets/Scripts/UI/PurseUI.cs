using UnityEngine;
using TMPro; 
using RPG.Inventories;
namespace RPG.UI
{
    
    public class PurseUI : MonoBehaviour {
        [SerializeField] TextMeshProUGUI balanceField;

        Purse playerPurse = null;
        private void Start() {
            playerPurse= GameObject.FindGameObjectWithTag("Player").GetComponent<Purse>();
            if(playerPurse !=null)
            {
                playerPurse.onChange+=RefreshUI;
            }
            RefreshUI();
        }

        private void RefreshUI()
        {
            balanceField.text=$"${playerPurse.GetBalance():N2}";
        }
    }
}