using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class U_Encoded : MonoBehaviour
{
    public static string ToUTF8(string unicodeString) 
    {
        UTF8Encoding utf8 = new UTF8Encoding(); 
        Byte[] encodedBytes = utf8.GetBytes(unicodeString); 
        String decodedString = utf8.GetString(encodedBytes); 
        return decodedString; 
    }


}
