using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using OlimpiadaCSharp.Models;

namespace OlimpiadaCSharp.Helpers
{
    public class FillVizualizareItinerariu
    {
        //populeaza tabela itinerariului
        public static DataTable Fill(DataTable initTable, DataTable tableToFill)
        {
            //selecteaza toate localitatile din baza
            List<LocationModel> locations = DataAcces.GetLocations();

            List<DayModel> days = new List<DayModel>();

            //transforma randurile din tabela in daymodel-ul corespunzator
            days = initTable.AsEnumerable().ToList().Select(x => DayModel.ParseFinal(x)).ToList();

            //adauga o coloana noua
            tableToFill.Columns.Add("Image");

            //adauga in itinerarui cate 1 zi
            foreach (DayModel day in days)
            {
                DateTime date = day.DataStart;

                while (date <= day.DataStop)
                {
                    DataRow row = tableToFill.NewRow();
                    row["Localitate"] = day.Nume;
                    row["Data"] = date;
                    date = date.AddDays(1);

                    var rez = locations.Find(x => x.Localitate == day.Nume);
                    List<string> imagini = DataAcces.GetImages(rez.Id);
                    string imagineDeAfisat = imagini.First();
                    if (tableToFill.Rows.Count > 0)
                    {
                        List<string> imaginiFolosite = tableToFill.AsEnumerable().ToList().Where(x => x.Field<string>("Localitate") == day.Nume).Select(x => x.Field<string>("Image")).ToList();

                        if (imaginiFolosite.Count >= imagini.Count)
                        {
                            var groupurideimag = imaginiFolosite.GroupBy(x => x).OrderBy(x => x.Count());
                            imagineDeAfisat = groupurideimag.First().First();
                        }
                        else
                        {
                            imagineDeAfisat = imagini.Find(x => !imaginiFolosite.Contains(x));
                        }
                    }

                    row["Image"] = imagineDeAfisat;

                    tableToFill.Rows.Add(row);
                }
            }
            //sorteza tablea dupa coloana data
            tableToFill.DefaultView.Sort = "Data ASC";

            return tableToFill;
        }
    }
}
