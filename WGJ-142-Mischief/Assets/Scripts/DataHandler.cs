using System.IO;
using System.Xml.Serialization;



public static class DataHandler
{
    #region data
    private const string root = "";
    #endregion data

    #region Binary file methods

    public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
    {
        using(Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
        {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            binaryFormatter.Serialize(stream, objectToWrite);
        }
    }

    public static T ReadFromBinaryFile<T>(string filePath)
    {
        try
        {
            using(Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
        catch
        {
            return default(T);
        }
    }

    #endregion binary file methods

    #region XML file methods

    public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
    {
        TextWriter writer = null;
        try
        {
            var serializer = new XmlSerializer(typeof(T));
            writer = new StreamWriter(filePath, append);
            serializer.Serialize(writer, objectToWrite);
        }
        catch
        {

        }
        finally
        {
            if(writer != null)
                writer.Close();
        }
    }

    public static T ReadFromXmlFile<T>(string filePath) where T : new()
    {
        TextReader reader = null;
        try
        {
            var serializer = new XmlSerializer(typeof(T));
            reader = new StreamReader(filePath);
            return (T)serializer.Deserialize(reader);
        }
        catch
        {
            return default(T);
        }
        finally
        {
            if(reader != null)
                reader.Close();
        }
    }

    #endregion XMl file methods

    #region Text file methods
    public static void WriteToTxt<T>(string filePath, T objectToWrite, bool append = false)
    {
        System.IO.File.WriteAllText(@filePath, objectToWrite.ToString());
    }
    public static string ReadFromText(string filePath)
    {
        return System.IO.File.ReadAllText(filePath);
    }
    #endregion Text file methods
}
