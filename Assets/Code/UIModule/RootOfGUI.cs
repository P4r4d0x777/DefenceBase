using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.UIModule
{
    public class RootOfGUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countMoney;
        [SerializeField] private GameObject restartMenu;
        private int money;

        public void ChangeMoney(int count)
        {
            money += count;

            countMoney.text = money.ToString();
        }

        public void ShowRestartMenu()
        {
            restartMenu.SetActive(true);
        }
        public void RestartGame()
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
    }
}