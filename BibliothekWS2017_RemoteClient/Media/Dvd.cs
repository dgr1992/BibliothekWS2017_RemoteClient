using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliothekWS2017_RemoteClient.Media
{
    public class Dvd
    {
        public string id { get; set; } = "";
        public string title { get; set; } = "";
        public string asin { get; set; } = "";
        public string releaseDate { get; set; } = "";
        public string publisher { get; set; } = "";
        public string director { get; set; } = "";

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
                   "asin : " + asin + "\n" +
                   "releaseDate : " + getDateFormated() + "\n" +
                   "publisher : " + publisher + "\n" +
                   "director : " + director + "\n" +
                   "---------------------";
        }
    }
}
