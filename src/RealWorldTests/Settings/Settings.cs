using System.Xml;

namespace RealWorldTests.Settings;

public static class Settings
{
    public static string File = "Settings.xml";

    private static string? _baseUrl;
    private static string? _apiUrl;

    private static readonly XmlDocument Document;

    static Settings()
    {
        if (!System.IO.File.Exists(File))
            throw new Exception("Problem: settings file not found: " + File);

        Document = new XmlDocument();
        Document.Load(File);
    }

    public static string BaseUrl
    {
        get
        {
            if (_baseUrl == null)
            {
                var node = Document.DocumentElement?.SelectSingleNode("BaseUrl");
                _baseUrl = node?.InnerText ?? throw new Exception("BaseUrl not found in Settings.xml");
            }
            return _baseUrl;
        }
    }

    public static string ApiUrl
    {
        get
        {
            if (_apiUrl == null)
            {
                var node = Document.DocumentElement?.SelectSingleNode("ApiUrl");
                _apiUrl = node?.InnerText ?? throw new Exception("ApiUrl not found in Settings.xml");
            }
            return _apiUrl;
        }
    }
}
