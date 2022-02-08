using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SinglePageContactApplication.RuntimePlugins;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using SinglePageContactApplication.Models.Data;
using SinglePageContactApplication.Models.Entities;

namespace SinglePageContactApplication.Controllers
{
    [ApiController, Route(Routes.ControllerRoute)]
    public class ContactController : ControllerBase
    {

        private readonly ContactDbContext _context;

        public ContactController(ContactDbContext context)
        {
            this._context = context;
        }
        
        [HttpGet]
        public Task GetContactPage()
        {
            return Response.SendFileAsync(Routes.ContactsPageRoute);
        }

        [HttpGet("{mode}")]
        public string Get(string mode)
        {
            return JsonSerializer.Serialize<object>(mode switch
            {
                GetModes.TableContent=>
                    this._context.Employees
                        .Include(e => e.Position)
                        .ToList(),
                GetModes.ContactContent=>
                    JsonSerializer.Deserialize<Contacts>(System.IO.File.ReadAllText(Routes.ContactsDataFile)),
                GetModes.JobTitlesContent=>
                    this._context.JobTitles.ToList(),
                _ => this._context.Employees
                    .Include(e => e.Position)
                    .First(e=>e.Id == uint.Parse(mode))
            });
        }

        [HttpDelete("{id}")]
        public void DeleteEmployee(uint id)
        {
            this._context.Employees.Remove(this._context.Employees.First(e => e.Id == id));
            this._context.SaveChanges();
        }

        [HttpPost]
        public void AddEmployee([FromBody]JsonElement employee)
        {
            this._context.Employees.Add(employee);
            this._context.SaveChanges();
        }

        [HttpPut("{Id}")]
        public void UpdateEmployee(uint id, [FromBody] JsonElement jsonEmployee)
        {
            var employee = this._context.Employees.First(e => e.Id == id);
            employee ^= jsonEmployee;   

            this._context.SaveChanges();
        }
        
    }
}