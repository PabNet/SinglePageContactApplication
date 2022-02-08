using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using SinglePageContactApplication.RuntimePlugins;

namespace SinglePageContactApplication.Models.Entities
{
    public class Employee
    {
        [Key, Column(TypeName = DataTypes.KeyType)]
        public uint Id { get; set; }

        [Column(FieldNames.NameField, TypeName = DataTypes.NameType)]
        public string Name { get; set; }

        [Column(FieldNames.PhoneField, TypeName = DataTypes.PhoneType)]
        public string PhoneNumber { get; set; }

        [Column(FieldNames.BirthDateField, TypeName = DataTypes.BirthDateType)]
        public DateTime BirthDate { get; set; }

        [Column(FieldNames.JobTitleForeignKeyField, TypeName = DataTypes.KeyType)]
        public uint JobTitleId { get; set; }

        public JobTitle Position { get; set; }

        public static implicit operator Employee(JsonElement json)
        {
            return new()
            {
                Name = json.GetProperty(FieldNames.NameField).ToString(),
                BirthDate = DateTime.Parse(json.GetProperty(FieldNames.BirthDateField).ToString()!),
                PhoneNumber = json.GetProperty(FieldNames.PhoneField).ToString(),
                JobTitleId = uint.Parse(json.GetProperty(FieldNames.JobTitleForeignKeyField).ToString()!),
            };
        }

        public static Employee operator ^(Employee employee, JsonElement element)
        {
            employee.Name = element.GetProperty(FieldNames.NameField).ToString();
            employee.PhoneNumber = element.GetProperty(FieldNames.PhoneField).ToString();
            employee.JobTitleId = uint.Parse(element.GetProperty(FieldNames.JobTitleForeignKeyField).ToString()!);
            employee.BirthDate = DateTime.Parse(element.GetProperty(FieldNames.BirthDateField).ToString()!);

            return employee;
        }
        
    }
}