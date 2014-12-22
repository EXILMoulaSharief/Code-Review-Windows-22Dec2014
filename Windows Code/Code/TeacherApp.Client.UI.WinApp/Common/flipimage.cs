using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherApp.Client.UI.WinApp
{
    public class flipimage
    {
        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private string imageurl;
        public string ImageUrl
        {
            get { return imageurl; }
            set { imageurl = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string pic;
        public string Pic
        {
            get { return pic; }
            set { pic = value; }
        }
        private string functionality;
        public string Functionality
        {
            get { return functionality; }
            set { functionality = value; }
        }
        public flipimage(string s, string sa,string str)
        {
            title = s;
            imageurl = sa;
            name = str;
        }
        public flipimage(string strin,string url)
        {
            name = strin;
            imageurl = url;
        }
        public flipimage(string s, string sa, string str, string stri)
        {
            name = s;
            imageurl = sa;
            pic = str;
            title = stri;           
        }
        public flipimage(string s, string sa, string str,string stri,string strin)
        {
            name = s;
            imageurl = sa;
            pic = str;
            title = stri;
            functionality = strin;
        }
    }
}
