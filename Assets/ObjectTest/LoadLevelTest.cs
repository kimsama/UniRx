using System;
using System.Collections;
using UnityEngine;
#if !(UNITY_4 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2)
using UnityEngine.SceneManagement;
#endif

namespace UniRx.ObjectTest
{
    public class LoadLevelTest : MonoBehaviour
    {
        void Start()
        {
            var ll = GameObject.Find("LoadLevel");
            var llcolor = ll.GetComponent<GUIText>().color;
            var cll = ll.AddComponent<Clicker>();
#if !(UNITY_4 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2)
            cll.OnClicked += () => SceneManager.LoadScene("LoadLevelTestNew");
#else
            cll.OnClicked += () => Application.LoadLevel("LoadLevelTestNew");
#endif
            cll.OnEntered += () => cll.GetComponent<GUIText>().color = Color.blue;
            cll.OnExited += () => cll.GetComponent<GUIText>().color = llcolor;

            var lla = GameObject.Find("LoadLevelAdditive");
            var llacolor = lla.GetComponent<GUIText>().color;
            var clla = lla.AddComponent<Clicker>();

#if !(UNITY_4 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2)
            clla.OnClicked += () => SceneManager.LoadScene("LoadLevelTestAdditive", LoadSceneMode.Additive);
#else
            clla.OnClicked += () => Application.LoadLevelAdditive("LoadLevelTestAdditive");
#endif
            clla.OnEntered += () => clla.GetComponent<GUIText>().color = Color.blue;
            clla.OnExited += () => clla.GetComponent<GUIText>().color = llacolor;
        }
    }
}
