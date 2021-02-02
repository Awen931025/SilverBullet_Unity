using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
public class U_Regex
{

    public static bool IsNumeric(string value)
    {
        Regex r = new Regex(@"^\d+$");
        return r.Match(value).Success;
    }
    public static bool IsInt(string value)
    {
        Regex r = new Regex(@"^[+-]?/d*$");
        return r.Match(value).Success;
    }
    public static bool IsUnsign(string value)
    {
        Regex r = new Regex(@"^/d*[.]?/d*$");
        return r.Match(value).Success;
    }
}
