using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Common
{
    [RequireComponent(typeof(Button))]
    public class LoadSceneOnButtonClick : MonoBehaviour
    {
        [SerializeField] private int seneIndex;

        private void Awake()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(LoadScene);
        }

        public void LoadScene()
        {
            SceneLoader.Instance.LoadScene(seneIndex);
        }
    }
}