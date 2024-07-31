/*
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
        public static async Task SeedAsync(SchoolDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {

                if (context.Levels != null && !context.Levels.Any())
                {
                    var levelsData = File.ReadAllText("../School.Repository/SeedData/Levels.json");
                    var levels = JsonSerializer.Deserialize<List<Level>>(levelsData);

                    if (levels is not null)
                    {
                        await context.Levels.AddRangeAsync(levels);

                        await context.SaveChangesAsync();
                    }
                }


                if (context.Departments != null && !context.Departments.Any())
                {
                    var departementssData = File.ReadAllText("../School.Repository/SeedData/Departements.json");
                    var departements = JsonSerializer.Deserialize<List<Department>>(departementssData);

                    if (departements is not null)
                    {
                        await context.Departments.AddRangeAsync(departements);

                        await context.SaveChangesAsync();
                    }
                }


                if (context.Terms != null && !context.Terms.Any())
                {
                    var termsData = File.ReadAllText("../School.Repository/SeedData/Terms.json");
                    var terms = JsonSerializer.Deserialize<List<Term>>(termsData);

                    if (terms is not null)
                    {
                        await context.Terms.AddRangeAsync(terms);

                        await context.SaveChangesAsync();
                    }
                }



                if (context.SchoolInfo != null && !context.SchoolInfo.Any())
                {
                    var schoolData = File.ReadAllText("../School.Repository/SeedData/SchoolInfo.json");
                    var school = JsonSerializer.Deserialize<List<SchoolInfo>>(schoolData);

                    if (school is not null)
                    {
                        await context.SchoolInfo.AddRangeAsync(school);

                        await context.SaveChangesAsync();
                    }
                }


            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}*/