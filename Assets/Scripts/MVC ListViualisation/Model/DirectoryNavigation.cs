using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
/*
 * -----------Class Description----------------
 * This script is the ovveriding behaviour for
 * the model class. This script is giving the
 * model file browsing logic.
 * 
 * This is the script that will contain anything
 * related to file browsing specifically.
 * --------------------------------------------
 */
public class DirectoryNavigation : Model
{
    public string pathToSearch;
    public GameObject renderTarget;


    [Header("Grahphical Icons")]
    public GameObject folderGFX;
    public GameObject imageGFX;
    public GameObject videoGFX;
    public GameObject documentGFX;
    public GameObject unknownFormatGFX;

    [Header("Known File Formats")]
    public string[] knownImageFileFormats = { ".gif", ".jpg", ".PNG", ".png" };
    public string[] knownVideoFileFormats = { ".mov", ".mp4", ".fmv" };
    public string[] knownDocumentFileFormats = { ".doc", ".pdf" };



    public override void BuildList()
    {
        base.BuildList();

        ClearPNG();

        if (!CheckValidPath(pathToSearch))
        {
            if (onInvalidRequest != null) onInvalidRequest.Invoke();
            return;
        }
        List<ListObjectInfo> newList = GenerateListObjectFromDirectory(pathToSearch);

        ListIsReady(newList);

    }
    public override void PreviousList()
    {
        base.PreviousList();

        if (MoveUpDirectory()) BuildList();
    }
    public override void ItemMoved(ListObjectInfo item)
    {
        base.ItemMoved(item);

        string destinationURL = pathToSearch + "/" + item.ObjectName;
        string originURL = item.ObjectInfo;

        if (!CheckValidPath(destinationURL))
        {
            if (onInvalidRequest != null) onInvalidRequest.Invoke(item.ObjectName);
            return;
        }

        Debug.Log("Moving, Origin: " + originURL + " Destination: " + destinationURL);

        File.Move(originURL, destinationURL);


        BuildList();

        ClearPNG();

    }
    public override void ItemDeleted(ListObjectInfo item)
    {
        base.ItemDeleted(item);

        Debug.Log("Deleting File: " + item.ObjectInfo);
        File.Delete(item.ObjectInfo);
        BuildList();

        ClearPNG();

    }
    public override void ItemSelected(ListObjectInfo item)
    {
        base.ItemSelected(item);

        ClearPNG();

        if (item.ObjectType.Equals("folder"))
        {
            //Debug.Log("Open Folder");

            if (!CheckValidPath(item.ObjectInfo))
            {
                if (onInvalidRequest != null) onInvalidRequest.Invoke(item.ObjectInfo);
                return;
            }

            pathToSearch = item.ObjectInfo;
            BuildList();
        }
        else
        {
            //Debug.Log("Open File");
            foreach (string ext in knownImageFileFormats)
            {
                if (item.ObjectType.Equals(ext))
                {
                    LoadPNG(item.ObjectInfo);
                    return;
                }
            }
        }

    }





    private bool CheckValidPath(string path)
    {
        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] fileInfo = null;
        DirectoryInfo[] dirInfo = null;

        try
        {
            fileInfo = dir.GetFiles();
            dirInfo = dir.GetDirectories();
        }
        catch (System.Exception e)
        {
            Debug.Log("Invalid Path: "+e);
            return false;
        }

        return true;
    }



    //modify the 'pathToSearch' var to go up a level.
    //Return true if moving up directory is sucessful.
    private bool MoveUpDirectory()
    {
        DirectoryInfo dir = new DirectoryInfo(pathToSearch);

        dir = Directory.GetParent(dir.ToString());

        if (dir != null)
        {
            //Debug.Log("Higher dir_" + dir.ToString());
            pathToSearch = dir.ToString();
            return true;
        }
        else
        {
            Debug.Log("Path Null");
            return false;
        }
       
    }



    //Return a list of files as'ListObjects' found at a directory.
    private List<ListObjectInfo> GenerateListObjectFromDirectory(string directory)
    {
        List<ListObjectInfo> generatedList = new List<ListObjectInfo>();

        DirectoryInfo dir = new DirectoryInfo(directory);
        FileInfo[] fileInfo = dir.GetFiles();
        DirectoryInfo[] dirInfo = dir.GetDirectories();



        foreach (DirectoryInfo d in dirInfo)
        {
            ListObjectInfo obj = new ListObjectInfo(d.Name, "folder", d.ToString(), folderGFX);
            generatedList.Add(obj);
        }
        foreach (FileInfo f in fileInfo)
        {
            ListObjectInfo obj = new ListObjectInfo(f.Name, f.Extension, f.ToString(), ChooseIcon(f.Extension, f.ToString()));
            generatedList.Add(obj);
        }


        return generatedList;
    }


    //Return a gameobject icon based on extention.
    private GameObject ChooseIcon(string fileExtension, string path)
    {
        //Check if extenstion is a known image file format.
        foreach(string knownFormat in knownImageFileFormats)
        {
            if (fileExtension.Equals(knownFormat))
            {
                return imageGFX;
            }
        }

        //Check if extenstion is a known video file format.
        foreach (string knownFormat in knownVideoFileFormats)
        {
            if (fileExtension.Equals(knownFormat))
            {
                return videoGFX;
            }
        }

        //Check if extenstion is a known document file format.
        foreach (string knownFormat in knownDocumentFileFormats)
        {
            if (fileExtension.Equals(knownFormat))
            {
                return documentGFX;
            }
        }

        //If extention is none of the above it is unknown.
        return unknownFormatGFX;
    }


    //SHould this be in the View?
    private void LoadPNG(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            renderTarget.GetComponent<Renderer>().material.mainTexture = tex;
        }

    }
    private void ClearPNG()
    {
        renderTarget.GetComponent<Renderer>().material.mainTexture=null;
    }

}
