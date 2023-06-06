using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace API.Helper
{
    public static class Formateador
    {
        public static string ToXml(this object input)
        {
            string output;
            XmlSerializer serializer = new XmlSerializer(input.GetType());
            StringBuilder sb = new StringBuilder();
            using (TextWriter textWriter = new StringWriter(sb))
            {
                serializer.Serialize(textWriter, input);
            }
            output = sb.ToString();
            return output;
        }
    }
}