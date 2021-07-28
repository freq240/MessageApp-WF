using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Message
{


    public partial class Form : System.Windows.Forms.Form
    {
        //------------------------------------------------------
        Own own = new Own();
        SMSmessage message = new SMSmessage();
        List<object> objList = new List<object>();
        bool stateSaved = false;
        //------------------------------------------------------
        public Form()
        {
            InitializeComponent();
            // Menu
            ToolStripMenuItem saveToFile = new ToolStripMenuItem("Save to file");
            saveToFile.Click += button4_Click;
            ToolStripMenuItem update = new ToolStripMenuItem("Update");
            update.Click += button3_Click;

            ToolStripMenuItem aboutItem = new ToolStripMenuItem("Info about program");
            aboutItem.Click += button5_Click;

            // adding
            menuStrip1.Items.Add(saveToFile);
            menuStrip1.Items.Add(update);
            menuStrip1.Items.Add(aboutItem);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            own.ReadFromFile(objList);
            string text = "";
            var selectedObjects = from t in objList
                                  select t;
            foreach (object obj in selectedObjects)
            {
                text += obj;
                text += "\n\n";
            }
            richTextBox1.Text = text;

            int maxLenghtMessage = 0;
            string typeMaxLenghtMessage = "";
            string maxLenghtText = "";
            foreach (SMSmessage obj in objList)
            {
                if (obj.GET_text().Length > maxLenghtMessage)
                {
                    maxLenghtMessage = obj.GET_text().Length;
                    typeMaxLenghtMessage = obj.GetType().ToString();
                    maxLenghtText = obj.GET_text();

                    if (typeMaxLenghtMessage == "Message.SMSmessage")
                    {
                        typeMaxLenghtMessage = "SMSmessage";
                    }
                    else
                    {
                        typeMaxLenghtMessage = "SMSmailing";
                    }
                }
            }
            textBox1.Text = Convert.ToString(maxLenghtMessage);
            textBox2.Text = typeMaxLenghtMessage;
            richTextBox2.Text = maxLenghtText;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 0 || textBox4.Text.Length == 0 || textBox5.Text.Length == 0)
            {
                if (textBox3.Text.Length == 0)
                {
                    textBox3.Text = "Fill!";
                }
                if (textBox4.Text.Length == 0)
                {
                    textBox4.Text = "Fill!";
                }
                if (textBox5.Text.Length == 0)
                {
                    textBox5.Text = "Fill!";
                }
            }
            else
            {
                stateSaved = false;
                this.button4.ForeColor = System.Drawing.Color.Red;
                SMSmessage myObj = own.CreateNewMessageObject(textBox3.Text, textBox4.Text, textBox5.Text);
                objList.Add(myObj);
                MessageBox.Show(
        "Message has been successfully added in collection!",
        "Success event!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string text = "";
            var selectedObjects = from t in objList
                                  select t;
            foreach (object obj in selectedObjects)
            {
                text += obj;
                text += "\n\n";
            }
            richTextBox1.Text = text;
        }


        private void button4_Click(object sender, EventArgs e)
        {
            stateSaved = true;
            this.button4.ForeColor = System.Drawing.Color.ForestGreen;
            Form dialogWindow = new Form();
            own.WriteInFile(objList);
            dialogWindow.Dispose();
            MessageBox.Show(
        "Message has been successfully added in collection!",
        "Success event!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Console.Beep();
            MessageBox.Show(
        "App: MessageApp\nAuthor: Andriy Kostiuk",
        "Info");

        }


        private void button6_Click(object sender, EventArgs e)
        {
            objList.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";

            richTextBox1.Text = "";
            richTextBox2.Text = "";
            richTextBox3.Text = "";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string number = textBox6.Text;
            List<string> allMessage = own.AllMessagesToRepicient(number, objList);
            if (allMessage.Count == 0)
            {
                Console.Beep();
                MessageBox.Show(
        "Messages to the abonent have not been founded!",
        ":(");
            }
            else
            {
                string text = "";
                foreach (string str in allMessage)
                {
                    text += str;
                    text += "\n";
                }
                richTextBox3.Text = text;
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox7.Text.Length == 0)
                {
                    textBox7.Text = "Fill!";
                }
                if (textBox8.Text.Length == 0)
                {
                    textBox8.Text = "Fill!";
                }

                int number = Convert.ToInt32(textBox8.Text);
                string newtext = textBox7.Text;

                own.ChangeText(number, newtext, objList);
                stateSaved = false;
                this.button4.ForeColor = System.Drawing.Color.Red;
                Console.Beep();
                MessageBox.Show(
        $"Text of message by {number} number has been changed!",
        "Success event!");
            }
            catch
            {
                textBox7.Text = "Error!";
                textBox8.Text = "Error!";
                MessageBox.Show(
        $"Data is not correct!",
        "Error!");
            }
        }

    }
 }
    

