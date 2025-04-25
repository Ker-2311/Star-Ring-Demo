using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exterior
{
    public static class UnityExterior
    {
        /// <summary>
        /// ��ȡ�������������û�������
        /// </summary>
        /// <typeparam name="T">�������</typeparam>
        /// <param name="panel">����</param>
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
        /// �������ϲ㸸����
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
        /// �����ֲ��Ҹ�����
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
        /// ͨ��·�������Ӷ�����������û�У��򴴽������
        /// </summary>
        /// <typeparam name="T">�������</typeparam>
        /// <param name="parent">������</param>
        /// <param name="path">�Ӷ�����Ը�����·��</param>
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
        /// ͨ��·�������Ӷ�������������
        /// </summary>
        /// <typeparam name="T">�������</typeparam>
        /// <param name="parent">������</param>
        /// <param name="path">�Ӷ���·��</param>
        /// <returns></returns>
        public static T FindComponent<T>(this GameObject parent, string path)
        {
            return parent.transform.Find(path).gameObject.GetComponent<T>();
        }

        /// <summary>
        /// ͨ��·�������Ӷ�����ΪGameobject����
        /// </summary>
        /// <param name="parent">������</param>
        /// <param name="path">�Ӷ�����Ը�����·��</param>
        /// <returns></returns>
        public static GameObject FindObject(this GameObject parent, string path)
        {
            return parent.transform.Find(path).gameObject;
        }

        /// <summary>
        /// ͨ�����Ʊ��������Ӷ�����ΪGameobject����
        /// </summary>
        /// <param name="parent">������</param>
        /// <param name="name">�Ӷ�������</param>
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
        /// ɾ������������
        /// </summary>
        /// <param name="parent">������</param>
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

