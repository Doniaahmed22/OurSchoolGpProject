using Microsoft.Extensions.Logging;
using School.Data.Context;
using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace School.Repository
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync (SchoolDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {

                if (context.Levels != null && !context.Levels.Any())
                {
                    var levelsData = File.ReadAllText("../School.Repository/SeedData/levels.json");
                    var levels = JsonSerializer.Deserialize<List<Level>>(levelsData);

                    if (levels is not null)
                    {
                        await context.Levels.AddRangeAsync( levels );

                        //await context.SaveChangesAsync();
                    }
                }


                if (context.Departments != null && !context.Departments.Any())
                {
                    var departementssData = File.ReadAllText("../School.Repository/SeedData/Departements.json");
                    var departements = JsonSerializer.Deserialize<List<Department>>(departementssData);

                    if (departements is not null)
                    {
                        await context.Departments.AddRangeAsync(departements);

                        //await context.SaveChangesAsync();
                    }
                }


                //if(context.Parents != null && !context.Parents.Any() )
                //{
                //    var parentsData = File.ReadAllText("../School.Repository/SeedData/parents.json");
                //    var parents = JsonSerializer.Deserialize<List<Parent>>(parentsData);

                //    if( parents is not null )
                //    {
                //        await context.Parents.AddRangeAsync(parents);

                //        //await context.SaveChangesAsync();
                //    }
                //}



                //if (context.Students != null && !context.Students.Any())
                //{
                //    var studentsData = File.ReadAllText("../School.Repository/SeedData/students.json");
                //    var students = JsonSerializer.Deserialize<List<Student>>(studentsData);

                //    if (students is not null)
                //    {
                //        await context.Students.AddRangeAsync(students);

                //        //await context.SaveChangesAsync();
                //    }
                //}

                //await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
