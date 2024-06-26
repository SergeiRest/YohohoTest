﻿using System;
using System.Linq;
using UnityEngine;

namespace Scripts.Player.Components
{
    [System.Serializable]
    public struct PlayerStack
    {
        public Transform[] Points;

        public void SetItem(Transform transform)
        {
            Transform emptyPoint = GetEmptyPoint();
            transform.SetParent(emptyPoint);
        }

        public string GetLast()
        {
            try
            {
                return Points.Last(point => point.childCount > 0).name;
            }
            catch (Exception e)
            {
                return "";
                //throw new Exception("A");
            }
        }

        public bool HasEmptyPoints()
        {
            try
            {
                if (Points.Count(point => point.childCount == 0) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Transform GetEmptyPoint()
        {
            return Points.First(transform => transform.childCount == 0);
        }
    }
}