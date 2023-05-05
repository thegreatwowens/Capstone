using System;
using System.Collections.Generic;

namespace DiZTools_AchievementsSystem
{
    public static class Utils
    {
        #region List stuff

        public static void AddAt<T>(this List<T> list, T item, int index)
        {
            list.Add(item);

            if (list.Count != index + 1)
            {
                for (int i = list.Count - 1; i > index; i--)
                {
                    list[i] = list[i - 1];
                }

                list[index] = item;
            }
        }

        public static void Switch<T>(this List<T> list, int index1, int index2)
        {
            T temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }

        public static void SetupListSize<T>(this List<T> list, int sizeToHave)
        {
            if (list.Count == sizeToHave)
                return;

            if (list.Count < sizeToHave)
            {
                while (list.Count != sizeToHave)
                    list.Add(default);

                return;
            }
            else if (list.Count > sizeToHave)
            {
                while (list.Count != sizeToHave)
                    list.RemoveAt(list.Count - 1);

                return;
            }
        }

        #endregion

        #region String stuff

        public static bool IsNullOrEmpty(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return true;

            if (string.IsNullOrEmpty(str.Replace(" ", "")))
                return true;

            return false;
        }

        public static bool AreSeveralAndAllNotNullOrEmpty(this string[] strArray)
        {
            if (strArray.Length <= 1)
                return false;

            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i].IsNullOrEmpty())
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Turn list like "{ 1st, 2nd, 3rd }" to "1st, 2nd and 3rd"
        /// </summary>
        /// <param name="listOfOrdinals"></param>
        /// <returns></returns>
        public static string ToEnumeration(this List<string> listOfOrdinals)
        {
            if (listOfOrdinals.Count == 0)
                return "";

            if (listOfOrdinals.Count == 1)
                return listOfOrdinals[0];

            if (listOfOrdinals.Count == 2)
                return listOfOrdinals[0] + " and " + listOfOrdinals[1];

            string res = listOfOrdinals[0];
            for (int i = 1; i < listOfOrdinals.Count - 1; i++)
            {
                res += (", " + listOfOrdinals[i]);
            }
            res += " and " + listOfOrdinals[listOfOrdinals.Count - 1];
            return res;
        }

        #endregion

        #region Numbers stuff

        public static string ToOrdinal(this int number)
        {
            string res = number.ToString();

            switch (number)
            {
                case 1:
                    res += "st";
                    break;
                case 2:
                    res += "nd";
                    break;
                case 3:
                    res += "rd";
                    break;
                default:
                    res += "th";
                    break;
            }

            return res;
        }

        #endregion

        #region Tuples stuff

        public static void CopyToDictionary(this List<StringBoolTuple> list, Dictionary<string, bool> dict)
        {
            dict.Clear();

            for (int i = 0; i < list.Count; i++)
            {
                dict.Add(list[i].item1, list[i].item2);
            }
        }

        public static void CopyToDictionary(this List<StringIntTuple> list, Dictionary<string, int> dict)
        {
            dict.Clear();

            for (int i = 0; i < list.Count; i++)
            {
                dict.Add(list[i].item1, list[i].item2);
            }
        }

        public static bool ExistsInTupleList(this string key, List<StringBoolTuple> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].item1.Equals(key))
                    return true;
            }
            return false;
        }

        public static bool ExistsInTupleList(this string key, List<StringIntTuple> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].item1.Equals(key))
                    return true;
            }
            return false;
        }

        #endregion
    }

    #region Class stuff

    public class Tuple<T1, T2>
    {
        public T1 item1;
        public T2 item2;

        public Tuple(T1 _item1, T2 _item2)
        {
            item1 = _item1;
            item2 = _item2;
        }
    }

    [Serializable]
    public class StringBoolTuple : Tuple<string, bool>
    {
        public StringBoolTuple(string _item1, bool _item2) : base(_item1, _item2) { }
    }

    [Serializable]
    public class StringIntTuple : Tuple<string, int>
    {
        public StringIntTuple(string _item1, int _item2) : base(_item1, _item2) { }
    }

    #endregion
}
