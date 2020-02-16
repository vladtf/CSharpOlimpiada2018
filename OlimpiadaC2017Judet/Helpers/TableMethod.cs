using OlimpiadaCSharp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace OlimpiadaCSharp.Helpers
{
    public class TableMethods
    {
        //primeste tabela cu toate planificarile si le salveaza doar pe celea care sunt intre cele 2 date selectate
        public static DataTable Fill(DataTable table, DateTime datastart, DateTime datastop)
        {
            List<DayModel> Rows = table.AsEnumerable().ToList().Select(x => DayModel.Parse(x)).ToList();

            table.Clear();

            foreach (DayModel day in Rows)
            {
                DataRow row = table.NewRow();
                DayModel newDay = new DayModel();
                newDay.Nume = day.Nume;
                newDay.Frecvneta = day.Frecvneta;

                //daca este ocazional atunci o adauga cu modificar a.i. sa se incadreze intre cele 2 date
                if (day.Frecvneta == "ocazional")
                {
                    //daca planificarea are cel putin 1 zi favorabila atunci o adauga
                    if (day.DataStart <= datastop && day.DataStart >= datastart || day.DataStop >= datastart && day.DataStop <= datastop)
                    {
                        //modifica planificarea a.i. sa corespunda intervalului ales
                        newDay.DataStart = new DateTime(Math.Max(day.DataStart.Ticks, datastart.Ticks));
                        newDay.DataStop = new DateTime(Math.Min(day.DataStop.Ticks, datastop.Ticks));

                        //creeaza randul respectiv
                        row = FillRow(row, newDay);
                        //adauga randul
                        table.Rows.Add(row);
                    }
                }
                //daca este anual
                else if (day.Frecvneta == "anual")
                {
                    //daca ziua data se incadreaza intre zilele anului corespunzatoare intervalului ales
                    if (datastart.DayOfYear <= day.Ziua && datastop.DayOfYear >= day.Ziua)
                    {
                        //creeaza data corespunzatoare zilei din an
                        DateTime date = new DateTime(datastart.Year, 1, 1).AddDays(day.Ziua - 1);

                        newDay.DataStart = newDay.DataStop = date;

                        //creeeaz si adauga randul
                        row = FillRow(row, newDay);
                        table.Rows.Add(row);
                    }
                }
                //daca este lunal
                else
                {
                    //creeaza data corespunzatoare zilei din luna
                    DateTime date = new DateTime(datastart.Year, datastart.Month, day.Ziua);

                    //verifica toate zilele care se afla intre luni intervalului
                    while (date <= datastop)
                    {
                        if (date >= datastart)
                        {
                            newDay.DataStart = newDay.DataStop = date;

                            row = table.NewRow();
                            row = FillRow(row, newDay);
                            table.Rows.Add(row);
                        }

                        date = date.AddMonths(1);
                    }
                }
            }

            return table;
        }

        //completeaza randul cu datele corespunzatoare
        public static DataRow FillRow(DataRow row, DayModel day)
        {
            row["Nume"] = day.Nume;
            row["DataStart"] = day.DataStart;
            row["DataStop"] = day.DataStop;
            row["Frecventa"] = day.Frecvneta;

            return row;
        }
    }
}