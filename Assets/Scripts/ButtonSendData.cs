using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ButtonSendData : MonoBehaviour
    {

        Button btn;
        public string query;

        private void Start()
        {
            btn = GetComponent<Button>();
            btn.onClick.AddListener(() => HandleDataForDB.instance.SetData(query));
        }
    }
}