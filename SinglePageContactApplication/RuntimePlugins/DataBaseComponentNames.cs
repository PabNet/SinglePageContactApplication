namespace SinglePageContactApplication.RuntimePlugins
{
    public struct DataBaseComponentNames
    {
        public const string DataBase = "Contacts",
            NameField = "Name",
            PhoneField = "PhoneNumber",
            BirthDateField = "BirthDate",
            JobTitleForeignKeyField = "JobTitleId",
            EmployeeTable = "Employees",
            JobTitleTable = "JobTitles";
    }
}