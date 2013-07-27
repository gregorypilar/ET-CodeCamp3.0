using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETConsole
{
    class Program
    {
        //private static void SpecifyDatabaseName()
        //{
        //    using (var context =
        //    new DbTest("ETConsole.Properties.Settings.Setting"))
        //    {
        //        context.Destinations.Add(new Destination { Name = "Tasmania" });
        //        context.SaveChanges();
        //    }
        //}

        static void Main(string[] args)
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DbTest>());


            using (var db = new DbTest())
            {
                try
                {
                    db.Test.Add(new Test()
                        {
                            Nombre = " Gregory Aj",
                            Apellido = " Pilar Ortiz",
                            Telefono = ""
                        });

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    foreach (var s in db.GetValidationErrors())
                    {
                        foreach (var s1 in s.ValidationErrors)
                        {
                            Console.WriteLine(s1.ErrorMessage);
                        }

                    }

                }

            }



            Console.Read();
        }
    }
}
