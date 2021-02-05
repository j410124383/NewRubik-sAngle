using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;

namespace YProjectBase
{

    /// <summary>
    /// 数据保存中心
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GameSaveManager<T> : BaseManager<GameSaveManager<T>> where T : Scriptablebase
    {

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="gameData"></param>
        /// <param name="callback"></param>
        public void SaveGame(Scriptablebase gameData, UnityAction<Scriptablebase> callback = null)
        {
            if (gameData == null) return;

            Debug.Log(Application.persistentDataPath);

            Scriptablebase data = gameData;

            string pathName = "ItemsData";

            pathName = (gameData as Scriptablebase).PathName;

            string datasName = (gameData as Scriptablebase).DataName;

            string path = Application.persistentDataPath + "/GameData/" + pathName;


            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = path + "/" + datasName + "_data.txt";

            //二进制转化
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream file = File.Create(path);

            var json = JsonUtility.ToJson(data);

            Debug.Log(json);

            formatter.Serialize(file, json);

            file.Close();
        }

        /// <summary>
        /// 加载存档
        /// </summary>
        /// <param name="_data"></param>
        /// <param name="callback"></param>
        public void LoadGame(T _data, UnityAction<T> callback = null)
        {
            if (_data == null) return;

            Scriptablebase data = _data;

            BinaryFormatter bf = new BinaryFormatter();

            string pathName = "ItemsData";

            pathName = (_data as Scriptablebase).PathName;
            string datasName = (_data as Scriptablebase).DataName;

            string path = Application.persistentDataPath + "/GameData/" + pathName + "/" + datasName + "_data.txt";


            if (File.Exists(path))
            {
                FileStream file = File.Open(path, FileMode.Open);

                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), data);

                callback?.Invoke(data as T);

                file.Close();
            }
            else
            {
                callback?.Invoke(null);
            }

        }

        /// <summary>
        /// 删除指定数据
        /// </summary>
        /// <param name="gameData"></param>
        public void DeleteFile(T gameData)
        {
            if (gameData == null) return;

            string pathName = "ItemsData";

            pathName = (gameData as Scriptablebase).PathName;

            string datasName = (gameData as Scriptablebase).DataName;

            string path = Application.persistentDataPath + "/GameData/" + pathName + "/" + datasName + "_data.txt";


            if (File.Exists(path))
            {

                File.Delete(path);
            }
        }

        /// <summary>
        /// 删除全部数据
        /// </summary>
        /// <param name="gameData"></param>
        public void DeleteAllFile()
        {

            string path = Application.persistentDataPath + "/GameData";

            if (Directory.Exists(path) == true)
            {
                Directory.Delete(path, true);
            }

        }


        //#region Android Java获取persistentDataPath以解决Unity获取路径为空的问题

        //private static string[] _persistentDataPaths;

        //public static bool IsDirectoryWritable(string path)
        //{
        //    try
        //    {
        //        if (!Directory.Exists(path)) return false;
        //        string file = Path.Combine(path, Path.GetRandomFileName());
        //        using (FileStream fs = File.Create(file, 1)) { }
        //        File.Delete(file);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //private static string GetPersistentDataPath(params string[] components)
        //{
        //    try
        //    {
        //        string path = Path.DirectorySeparatorChar + string.Join("" + Path.DirectorySeparatorChar, components);
        //        if (!Directory.GetParent(path).Exists) return null;
        //        if (!Directory.Exists(path))
        //        {
        //            Debug.Log("creating directory: " + path);
        //            Directory.CreateDirectory(path);
        //        }
        //        if (!IsDirectoryWritable(path))
        //        {
        //            Debug.LogWarning("persistent data path not writable: " + path);
        //            return null;
        //        }
        //        return path;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Debug.LogException(ex);
        //        return null;
        //    }
        //}

        //#endregion

    }
}