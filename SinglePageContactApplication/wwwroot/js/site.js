const serverURL = "/contacts";
let modes = {TableEntries: "Table",
             ContactDetails: "Contacts",
             JobTitleList: "JobTiTleList",
             EmployeeId: "0"},
employeePosition = '', employeeId = 0;

$(document).ready(() => {
    
    GetAllContacts();
    getDataFromServer(modes.ContactDetails);

    function GetAllContacts()
    {
        getDataFromServer(modes.TableEntries);
    }
    
     function getDataFromServer(mode)
     {
         $.ajax(
             {
                 url: serverURL + `/${mode}`,
                 method: 'get',
                 dataType: 'json',
                 async: false,
                 success: (data)=>{
                     switch (mode)
                     {
                         case modes.TableEntries:
                         {
                             fillInTheTable(data);
                             break;
                         }
                         case modes.ContactDetails:
                         {
                             fillInContactDetails(data);
                             break;
                         }
                         case modes.JobTitleList:
                         {
                             completeTheJobList(data);
                             break;
                         }
                         default:
                         {
                             fillInTheUserData(data);
                             break;
                         }
                     }
                 }
             }
         );
     }
     
     function fillInTheUserData(employee)
     {
         $("#Name").val(employee.Name);
         $("#Phone").val(employee.PhoneNumber);
         $("#BirthDate").val(employee.BirthDate);
         employeePosition = employee.Position.Position;
     }
     
     function completeTheJobList(data)
     {
         let jobList = "<option selected disabled>Job Title</option>";
         $.each(data, (index, jobTitle)=>{
             jobList += `<option value='${jobTitle.Id}'>` + jobTitle.Position + "</option>"
         });
         $("#JobTitleChoice").empty().append(jobList);
     }
     
     function fillInContactDetails(data)
     {
         let contactContent = "<span>" + data.Phone + "</span>" + "<br>" +
             "<span>" + data.Address + "</span>" + "<br>" +
             "<span>" + data.Email + "</span>" + "<br>";
         $("#ContactDates").append(contactContent);
     }
     
     function fillInTheTable(entries)
     {
         let tableContent = '';
         $.each(entries, (index, entry)=>{
             tableContent += `<tr id=${entry.Id}>` +
             "<td>" + entry.Name + "</td>" +
             "<td>" + entry.PhoneNumber + "</td>" +
             "<td>" + entry.Position.Position + "</td>" +
             "<td>" + entry.BirthDate.substr(0, 10) + "</td>" +
             "</tr>"
         });
         $("#ContactsTable tbody").empty().append(tableContent);
     }
     
     function processDataOnTheServer(url, method, dataToJson)
     {
         $.ajax(
             {
                 url: url,
                 method: method,
                 async: false,
                 data: JSON.stringify(dataToJson),
                 contentType: "application/json;charset=utf-8",
                 success: ()=>{
                     GetAllContacts();
                     CloseModal();
                 }
             }
         );
     }

    $(document).keydown((e)=>
    {
        if (e.keyCode === 27) 
        {
            RemoveColorLines();
        }
    });

    $('tbody tr').on("click", (event)=>
    {
        RemoveColorLines();
        event.currentTarget.className =  "Selected";
    });
    
    function RemoveColorLines()
    {
        $("tbody tr").removeClass("Selected");
    }

    $('#AddButton').on("click", (event)=>
    {
        $("#ConfirmButton").text("Add");
        OpenModal();
    });

    $('#DeleteButton').on("click", ()=>
    {
        $.ajax(
            {
                url: serverURL + `/${$(".Selected").attr("id")}`,
                method: 'delete',
                async: false,
                success: ()=>{
                    location.reload();
                }
            }
        );
    });

    $('#EditButton').on("click", (event)=>
    {
        modes.EmployeeId = employeeId = $(".Selected").attr("id");
        getDataFromServer(String(modes.EmployeeId));
        $("#ConfirmButton").text("Save");
        OpenModal();
        selectPosition();
    });

    $('#ConfirmButton').on("click", (event)=>
    {
        const
            method = ($("#ConfirmButton").text() === 'Add') ? 'post' : 'put',
            data = {
            Name: $("#Name").val(),
            PhoneNumber: $("#Phone").val(),
            JobTitleId: $('#JobTitleChoice').val(),
            BirthDate: $('#BirthDate').val(),},
        url = serverURL + ((method === 'post') ? '' : `/${employeeId}`);
        processDataOnTheServer(url, method, data);
    });
    
    $(".close").on("click", ()=>{
        CloseModal();
    });
    
    function OpenModal()
    {
        getDataFromServer(modes.JobTitleList);
        $("#AdditionalTask").css("display", "block");
    }
    
    function CloseModal()
    {
        $("#AdditionalTask").css("display", "none");
    }
    
    function selectPosition()
    {
        $("#JobTitleChoice option").each((index, option)=>{
            option.selected = (option.text === String(employeePosition)) ? "selected" : "";
        });
    }
    
    
});