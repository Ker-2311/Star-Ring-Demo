using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugModePanel : MonoBehaviour
{
    private InputField _input;
    private void Awake()
    {
        _input = GetComponent<InputField>();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)&& _input.text!=null)
        {
            var text = _input.text.Split(' ');
            var command = text[0];
            if(DebugMgr.Instance.Commands.ContainsKey(command))
            {
                if (text.Length == 1)
                {
                    DebugMgr.Instance.Commands[command](new object[] { });
                }
                else if (text.Length == 2)
                {
                    DebugMgr.Instance.Commands[command](new object[] { text[1]});
                }
            }
            else
            {
                Debug.Log(" ‰»Î¥ÌŒÛ÷∏¡Ó");
            }
            _input.text = null;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.RemoveUI(gameObject);
        }
    }
}
