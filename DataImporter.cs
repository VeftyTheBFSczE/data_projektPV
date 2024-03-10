using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace Projekt
{
  
        public class DataImporter
        {
            public void ImportProducts(string filePath)
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var products = csv.GetRecords<Product>().ToList();
                    
                }
            }
        }

    }

