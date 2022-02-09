using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using SinglePageContactApplication.RuntimePlugins;

namespace SinglePageContactApplication.Models.Entities
{
    [Table(DataBaseComponentNames.EmployeeTable)]
    public class Employee
    {
        [Key, Column(TypeName = DataTypes.KeyType)]
        public uint Id { get; set; }

        [Column(DataBaseComponentNames.NameField, TypeName = DataTypes.NameType)]
        public string Name { get; set; }

        [Column(DataBaseComponentNames.PhoneField, TypeName = DataTypes.PhoneType)]
        public string PhoneNumber { get; set; }

        [Column(DataBaseComponentNames.BirthDateField, TypeName = DataTypes.BirthDateType)]
        public DateTime BirthDate { get; set; }

        [Column(DataBaseComponentNames.JobTitleForeignKeyField, TypeName = DataTypes.KeyType)]
        public uint JobTitleId { get; set; }

        public JobTitle Position { get; set; }

        public static implicit operator Employee(JsonElement json)
        {
            return new()
            {
                Name = json.GetProperty(DataBaseComponentNames.NameField).ToString(),
                BirthDate = DateTime.Parse(json.GetProperty(DataBaseComponentNames.BirthDateField).ToString()!),
                PhoneNumber = json.GetProperty(DataBaseComponentNames.PhoneField).ToString(),
                JobTitleId = uint.Parse(json.GetProperty(DataBaseComponentNames.JobTitleForeignKeyField).ToString()!),
            };
        }

        public static Employee operator +(Employee employee, JsonElement element)
        {
            employee.Name = element.GetProperty(DataBaseComponentNames.NameField).ToString();
            employee.PhoneNumber = element.GetProperty(DataBaseComponentNames.PhoneField).ToString();
            employee.JobTitleId = uint.Parse(element.GetProperty(DataBaseComponentNames.JobTitleForeignKeyField).ToString()!);
            employee.BirthDate = DateTime.Parse(element.GetProperty(DataBaseComponentNames.BirthDateField).ToString()!);

            return employee;
        }
        
    }
}