﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace _3_praktinė_užduotis
{
    public partial class Form1 : Form
    {
        private RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(512);
        private RSAParameters privatekey;
        private RSAParameters publickey;
        public Form1()
        {
            InitializeComponent();
            publickey = RSA.ExportParameters(false);
            privatekey = RSA.ExportParameters(true);
        }

     

        private void button1_Click(object sender, EventArgs e)
        {

            RSA.PersistKeyInCsp = false;
            RSA.ImportParameters(publickey);
            var data = Encoding.Unicode.GetBytes(textBox1.Text);
            var cypher = RSA.Encrypt(data, false);
            textBox2.Text = Convert.ToBase64String(cypher);

            using (StreamWriter writer = new StreamWriter("Result.txt"))
            {
                writer.WriteLine(textBox2.Text);
            }

           /* using (StreamWriter writer = new StreamWriter("Publickey.txt"))
            {
                writer.WriteLine(publickey);
            }*/

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            var dataBytes = Convert.FromBase64String(textBox2.Text);
            RSA.ImportParameters(privatekey);
            var result = RSA.Decrypt(dataBytes, false);
            textBox4.Text = Encoding.Unicode.GetString(result);

            using (StreamWriter writer = new StreamWriter("privatekey.txt"))
            {
                writer.WriteLine(publickey);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("Result.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            textBox2.Text = sr.ReadToEnd();

            sr.Close();
            fs.Close();
        }
    }
}
