using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kharazmi.Editor
{
    public class ClearCache
    {

        [MenuItem("Tools/Clear Cache", false, 12)]
        public static async void ClearUserCache()
        {
            PlayerPrefs.DeleteAll();

        }
    }
}
