using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using TradeCategoryQuestion.Interfaces;

namespace TradeCategoryQuestion.Classes
{
    class Trade : ITrade
    {
        private double _value;
        public double Value => _value;

        private string _clientSector;
        public string ClientSector => _clientSector;

        private DateTime _nextPaymenteDate;
        public DateTime NextPaymentDate => _nextPaymenteDate;

        private string _category;
        public string Category => _category;


        public Trade(string[] args)
        {
            _value = double.Parse(args[0]);
            _clientSector = args[1];
            _nextPaymenteDate = DateTime.Parse(args[2]);

            _category = GetTradeCategory();
        }



        private string GetTradeCategory()
        {
            StringBuilder category = new();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Path.Combine(Util.GetApplicationPath(), "tradecategories.xml"));

            XmlNodeList nodeList = xmlDoc.SelectNodes("/categories/category[@sector='" + _clientSector + "']");

            foreach (XmlNode node in nodeList)
            {
                double minValue = double.Parse(node.Attributes.GetNamedItem("minValue").Value);
                double maxValue = double.Parse(node.Attributes.GetNamedItem("maxValue").Value);

                if (minValue == 0 && maxValue == 0)
                    category.AppendLine(node.Attributes.GetNamedItem("name").Value.ToString());
                else
                {
                    if (maxValue == 0) maxValue = double.MaxValue;

                    if (_value > minValue && _value < maxValue)
                        category.AppendLine(node.Attributes.GetNamedItem("name").Value.ToString());
                }
            }

            return category.ToString().Replace(Environment.NewLine, ", ");
        }



    }
}
