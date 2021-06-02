using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;


namespace CryptocurrencyApplication
{
    class PDF
    {
        private string path = "temp\\CryptoCurrency.pdf";
        private string pdfFilePath = "";
        Document document;
        public void createDocument(dynamic[] coins)
        {
            this.pdfFilePath = path;
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            this.pdfFilePath = projectDirectory + "\\" + this.pdfFilePath;

            Console.WriteLine(this.pdfFilePath);

            this.document = new Document();
            if (!Directory.Exists(projectDirectory + "\\temp"))
            {
                Directory.CreateDirectory(projectDirectory + "\\temp");
            }
            if(File.Exists(this.pdfFilePath))
            {
                File.Delete(this.pdfFilePath);
            }
            PdfWriter.GetInstance(document, new FileStream(this.pdfFilePath, FileMode.Create));
            document.Open();

            Paragraph paragraph = new Paragraph("Data");
            foreach (dynamic coin in coins)
            {
                double coinPrice = coin["quote"]["USD"]["price"];
                coinPrice = Math.Round(coinPrice, 2);
                string coinName = coin["name"];

                paragraph = new Paragraph(coinName + " : " + coinPrice + "$");
                document.Add(paragraph);
            }
            
            document.Close();
        }
        public PDF()
        {
        }
    }
}
