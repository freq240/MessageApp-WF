using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

namespace Message
{
    [Serializable]
    class Own
    {
        // DISPLAY EVENTS 
        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void DisplayRedMessage(string message)
        {
            // Red color
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            
            Console.ResetColor();
        }

        public SMSmessage CreateNewMessageObject(string senderNumber, string recepientNumber, string text)
        {
            return new SMSmessage(senderNumber, recepientNumber, text);
        }

        // FILE WORK
        public void ReadFromFile(List<object> objList)
        {
            string path = Path.GetFullPath("indata.txt");
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(file);

            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    break;
                }

                ObjectCreating(objList, line);
            }

            reader.Close();
            file.Close();
        }

        public void WriteInFile(List<object> objList)
        {
            object[] objects = new object[] { };
            foreach (object obj in objList)
            {
                objects.Append(obj);
            }

            // .txt
            string path = Path.GetFullPath("outdata.txt");
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);

            foreach (object obj in objList)
            {
                writer.Write(obj.ToString());
            }

            writer.Close();
            file.Close();

            //.dat
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs1 = new FileStream("outdata.dat", FileMode.Create);
            bf.Serialize(fs1, objects);
            fs1.Close();


            // create SoapFormatter
            SoapFormatter formatter = new SoapFormatter();
            // get stream
            
   
            using (FileStream fs2 = new FileStream("outdata.soap", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs2, objects);
 
                Console.WriteLine("Объект сериализован");
            }
        }
        public void ObjectCreating(List<object> objList, string line)
        {
            string[] subline = line.Split(';');
            if (subline.Length - 1 == 3)
            {
                SMSmessage obj = new SMSmessage(subline[0], subline[1], subline[2]);
                objList.Add(obj);
            }
            else
            {
                if (subline.Length - 1 == 4)
                {
                    SMSmailing obj = new SMSmailing(subline[0], subline[1], subline[2], subline[3]);
                    objList.Add(obj);
                }
                else
                {
                    SMSmailing obj = new SMSmailing(subline[0], subline[1], subline[2], subline[3], subline[4]);
                    objList.Add(obj);
                }

            }
        }

        // All message to some recepient

        public List<string> AllMessagesToRepicient(string recepient, List<object> objList)
        {

            List<string> tempList = new List<string>();
            foreach (SMSmessage obj in objList)
            {
                if (obj.GET_recepientNumber() == recepient)
                {
                    tempList.Add(obj.GET_text());
                }
                else
                {
                    continue;
                }

            }

            return tempList;
        }
        public void ChangeText(int number, string text, List<object> objList)
        {
            int counter = 0;
            foreach (SMSmessage obj in objList)
            {
                counter++;
                if (counter == number)
                {
                    obj.SET_text(text);
                }
                if (counter == number + 1)
                {
                    break;
                }
            }
        }
    }
}
