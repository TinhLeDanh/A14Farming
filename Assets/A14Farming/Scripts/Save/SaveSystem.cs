//using Firebase.Storage;
//using Firebase;
//using Firebase.Extensions;
using System.IO;
using UnityEngine;
using System;

public static class SaveSystem
{
    private static readonly string SaveFolder = Application.dataPath + "/Saves/";

    //private static FirebaseStorage storage;
    //private static StorageReference storageReference;

    public static void Init()
    {
        //storage = FirebaseStorage.DefaultInstance;
        //if (!Directory.Exists(SaveFolder))
        //{
        //    Directory.CreateDirectory(SaveFolder);
        //}
    }

    public static void Save(string Id, string filename, string saveString)
    {
        //storageReference = storage.GetReferenceFromUrl("gs://dfarmproject-56d7f.appspot.com/json_data");
        //string localFile = SaveFolder + $"{Id}_{filename}.txt";
        //File.WriteAllText(SaveFolder + Id + "_" + filename + ".txt", saveString);
        //StorageReference saveRef = storageReference.Child($"json_data/{Id}_{filename}");

        //var newMetadata = new MetadataChange();
        //newMetadata.ContentType = "json_data/txt";

        //saveRef.PutFileAsync(localFile).ContinueWithOnMainThread((task) => {
        //    if (task.IsFaulted || task.IsCanceled)
        //    {
        //        Debug.Log(task.Exception.ToString());
        //    }
        //    else
        //    {
        //    }
        //});
    }

    public static string Load(string Id, string file_name)
    {
        //DirectoryInfo directoryInfo = new DirectoryInfo(SaveFolder);
        //FileInfo[] savedFile = directoryInfo.GetFiles($"{Id}_{file_name}.txt");
        //FileInfo mostRecentFile = null;
        //foreach (FileInfo fileInfo in savedFile)
        //{
        //    if (mostRecentFile == null)
        //    {
        //        mostRecentFile = fileInfo;
        //    }
        //    else
        //    if (fileInfo.LastWriteTime > mostRecentFile.LastWriteTime)
        //        mostRecentFile = fileInfo;
        //}
        //if (mostRecentFile != null)
        //{
        //    string saveString = File.ReadAllText(mostRecentFile.FullName);
        //    return saveString;
        //}
        //else
        return null;
    }

}
