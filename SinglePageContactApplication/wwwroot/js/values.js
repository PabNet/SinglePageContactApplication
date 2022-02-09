const serverURL = "/contacts",
    regularExpressions = {
        DateCheck: "(19|20)\\d{2}-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1])",
            PhoneNumberCheck: "^(25|29|33|44)\\d{7}$",
        NameCheck: "^[A-Z][a-z]+ [A-Z][a-z]+$"
    },
errorText = "incorrect data!";

let modes = {TableEntries: "Table",
        ContactDetails: "Contacts",
        JobTitleList: "JobTiTleList",
        EmployeeId: "0"},
    employeePosition, employeeId, clickFlag;