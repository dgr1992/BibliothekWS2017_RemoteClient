using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliothekWS2017_RemoteClient.Media
{
    public class Book
    {
        public string id { get; set; } = "";
        public string title { get; set; } = "";
        public string isbn { get; set; } = "";
        public string releaseDate { get; set; } = "";
        public string publisher { get; set; } = "";
        public string author { get; set; } = "";

        public string getDateFormated()
        {
            long releaseDateLong = 0;
            long.TryParse(releaseDate, out releaseDateLong);

            TimeSpan time = TimeSpan.FromMilliseconds(releaseDateLong);
            DateTime date = new DateTime(1970, 1, 1) + time;

            return date.ToString("dd.MM.yyyy");
        }

        public override string ToString()
        {
            return "---------------------\n" +
                   "titel : " + title + "\n" +
                   "isbn : " + isbn + "\n" +
                   "releaseDate : " + getDateFormated() + "\n" +
                   "publisher : " + publisher + "\n" +
                   "author : " + author + "\n" +
                   "---------------------";
        }
    }
}
