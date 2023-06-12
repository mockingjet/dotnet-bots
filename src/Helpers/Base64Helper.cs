namespace app.Helpers;


public class Base64Helper
{
    public static string GetBase64String(string value)
    {
        byte[] plainBytes = System.Text.Encoding.UTF8.GetBytes(value);
        string base64Encoded = Convert.ToBase64String(plainBytes);
        return base64Encoded;
    }

    public static string GetStringValue(string base64String)
    {
        byte[] base64EncodedBytes = Convert.FromBase64String(base64String);
        string plainText = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        return plainText;
    }

    public static string GetBase64String(int value)
    {
        byte[] numberBytes = BitConverter.GetBytes(value);
        string base64Encoded = Convert.ToBase64String(numberBytes);
        return base64Encoded;
    }


    public static int GetIntValue(string base64String)
    {
        byte[] base64EncodedBytes = Convert.FromBase64String(base64String);
        int number = BitConverter.ToInt32(base64EncodedBytes);
        return number;
    }
}