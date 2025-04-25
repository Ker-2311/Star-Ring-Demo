using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exterior
{
    public static class UnityExterior
    {
        /// <summary>
        /// 获取对象的组件，如果没有则添加
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="panel">对象</param>
        /// <returns></returns>
        public static T FindOrAddComponent<T>(this GameObject panel) where T : Component
        {
            if (panel.GetComponent<T>() == null)
            {
                panel.AddComponent<T>();
            }
            return panel.GetComponent<T>();
        }

        /// <summary>
        /// 查找最上层父对象
        /// </summary>
        /// <returns></returns>
        public static GameObject FindUpParent(this GameObject gameObject)
        {
            if (gameObject.transform.parent == null)
            {
                return gameObject;
            }
            else
            {
                return FindUpParent(gameObject.transform.parent.gameObject);
            }
        }

        /// <summary>
        /// 按名字查找父对象
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject FindParentInName(this GameObject gameObject,string name)
        {
            var parent = gameObject.transform.parent;
            if (parent == null)
            {
                return null;
            }
            else if (parent.name == name)
            {
                return parent.gameObject;
            }
            return parent.gameObject.FindParentInName(name);
        }

        /// <summary>
        /// 通过路径查找子对象组件，如果没有，则创建该组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="parent">父对象</param>
        /// <param name="path">子对象相对父对象路径</param>
        /// <returns></returns>
        public static T FindOrAddComponent<T>(this GameObject parent, string path) where T:Component
        {
            var subObject = parent.transform.Find(path).gameObject;
            if (subObject.GetComponent<T>() == null)
            {
                subObject.AddComponent<T>();
            }
            return subObject.GetComponent<T>();
        }

        /// <summary>
        /// 通过路径查找子对象的组件并返回
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="parent">父对象</param>
        /// <param name="path">子对象路径</param>
        /// <returns></returns>
        public static T FindComponent<T>(this GameObject parent, string path)
        {
            return parent.transform.Find(path).gameObject.GetComponent<T>();
        }

        /// <summary>
        /// 通过路径查找子对象作为Gameobject返回
        /// </summary>
        /// <param name="parent">父对象</param>
        /// <param name="path">子对象相对父对象路径</param>
        /// <returns></returns>
        public static GameObject FindObject(this GameObject parent, string path)
        {
            return parent.transform.Find(path).gameObject;
        }

        /// <summary>
        /// 通过名称遍历查找子对象作为Gameobject返回
        /// </summary>
        /// <param name="parent">父对象</param>
        /// <param name="name">子对象名称</param>
        /// <returns></returns>
        public static GameObject FindChildObject(this GameObject parent, string name)
        {
            foreach(Transform child in parent.transform.GetComponentInChildren<Transform>())
            {
                if (child.name == name)
                {
                    return child.gameObject;
                }
            }
            return null;
        }

        /// <summary>
        /// 删除所有子物体
        /// </summary>
        /// <param name="parent">父对象</param>
        public static void DestroyChilds(this GameObject parent)
        {
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                GameObject.Destroy(parent.transform.GetChild(i).gameObject);
            }
        }

        public static List<GameObject> GetAllChilds(this GameObject parent)
        {
            List<GameObject> childs = new List<GameObject>();
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                childs.Add(parent.transform.GetChild(i).gameObject);
            }
            return childs;
        }
    }
}

