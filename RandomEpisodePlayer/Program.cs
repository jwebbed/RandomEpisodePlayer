using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using tvlib;

namespace RandomEpisodePlayer
{
    class Program
    {

        static void Main(string[] args)
        {
            ShowManager manager;
            string FileName = Directory.GetCurrentDirectory();
            FileName += "\\ShowData.data";
            if (File.Exists(FileName))
            {
                Stream ReadStream = File.OpenRead(FileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                manager = (ShowManager)deserializer.Deserialize(ReadStream);
                manager.playRandomEpisode("Community");
                ReadStream.Close();

                Stream WriteStream = File.OpenWrite(FileName);
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(WriteStream, manager);
                WriteStream.Close();
            }
            else
            {
                manager = new ShowManager("J:\\TV Shows");
                manager.Populate();
                manager.playRandomEpisode("Community");
                Stream s = File.Create(FileName);
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(s, manager);
                s.Close();
            }           
        }
    }
}
