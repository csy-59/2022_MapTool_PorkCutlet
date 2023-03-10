using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CSVParse<T>
{
    private static readonly char _columSeparator = ',';
    private static readonly char _rowSeparator = '\n';

    public static bool ArrayOf2DToCSV(T[][] data, int columSize, string fileName)
    {
        string path = $"{Application.dataPath}/Resources/{fileName}.csv";
        if(!File.Exists(path))
        {
            File.Create(path);
        }

        using (StreamWriter streamWriter = new StreamWriter(path))
        {
            foreach (T[] row in data)
            {
                string newText = string.Empty;
                for (int i = 0; i < columSize; ++i)
                {
                    if (i < row.Length)
                    {
                        newText += row[i].ToString();
                    }
                    newText += _columSeparator;
                }
                newText = newText.TrimEnd(_columSeparator);
                streamWriter.WriteLine(newText);
            }

        }

        return true;
    }

    public static string[][] CSVToArrayOf2D(string fileName)
    {
        string path = $"{Application.dataPath}/Resources/{fileName}.csv";
        if(File.Exists(path))
        {
            string text = new StreamReader(path).ReadToEnd();

            string[][] newArray;
            string[] stringText = text.TrimEnd(_rowSeparator).Split(_rowSeparator);
            newArray = new string[stringText.Length][];
            for (int i = 0; i < stringText.Length; ++i)
            {
                string[] row = stringText[i].TrimEnd('\r').Split(_columSeparator);

                newArray[i] = new string[row.Length];
                for (int j = 0; j < row.Length; ++j)
                {
                    newArray[i][j] = row[j];
                }
            }

            return newArray;
        }

        return null;
    }
}
