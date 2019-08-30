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
    public string[] knownVideoFileFormats = { ".mov", "mp4", ".fmv" };
    public string[] knownDocumentFileFormats = { ".doc", ".pdf" };



    public override void BuildList()
    {
        base.BuildList();

        List<ListObjectInfo> newList = GenerateListObjectFromDirectory(pathToSearch);

        ListIsReady(newList);
    }
    public override void PreviousList()
    {
        base.PreviousList();

        if (MoveUpDirectory()) BuildList();
    }
    public override void ItemSelected(ListObjectInfo item)
    {
        base.ItemSelected(item);

        if (item.ObjectType.Equals("folder"))
        {
            FolderSelected(item);
        }
        else
        {
            FileSelected(item);
        }
    }
    public override void ItemHovered(ListObjectInfo item)
    {
        base.ItemHovered(item);
    }



    private void FolderSelected(ListObjectInfo folder)
    {
        //Debug.Log("Open Folder");
        pathToSearch = folder.ObjectInfo;
        BuildList();
    }
    private void FileSelected(ListObjectInfo file)
    {
        Debug.Log("Open File");
        LoadPNG(file.ObjectInfo);
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

}
