$(document).ready(() => {

    getDataFromServer(modes.TableEntries);
    getDataFromServer(modes.ContactDetails);
    clickFlag = false;
    
    
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
         $("#BirthDate").val(cutDate(employee.BirthDate));
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
             "<td>" + cutDate(entry.BirthDate) + "</td>" +
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
                     closeModal();
                     location.reload();
                 }
             }
         );
     }

    $(document).keydown((e)=>
    {
        if (e.keyCode === 27) 
        {
            $("#ErrorMessage").text("");
            RemoveColorLines();
        }
    });

    $('tbody tr').click((event)=>
    {
        RemoveColorLines();
        event.currentTarget.className =  "Selected";
        clickFlag = true;
    });
    
    function RemoveColorLines()
    {
        $("tbody tr").removeClass("Selected");
        clickFlag = false;
    }

    $('#AddButton').on("click", ()=>
    {
        resetInputFields();
        $("#AdditionalTaskModal h2").text("Adding");
        $("#ConfirmButton").text("Add");
        openModal();
    });
    
    function resetInputFields()
    {
        $("input, select").val("");
    }

    $('#DeleteButton').on("click", ()=>
    {
        if(checkFlag())
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
        }
    });

    $('#EditButton').on("click", (event)=>
    {
        if(checkFlag())
        {
            modes.EmployeeId = employeeId = $(".Selected").attr("id");
            getDataFromServer(String(modes.EmployeeId));
            $("#AdditionalTaskModal h2").text("Editing");
            $("#ConfirmButton").text("Save");
            openModal();
            selectPosition();
        }
    });

    $('#ConfirmButton').on("click", (event)=>
    {
       if(checkData())
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
       }
    });
    
    $(".close").on("click", ()=>
    {
        closeModal();
    });
    
    function openModal()
    {
        $("header, footer, main").css("opacity", "0.3");
        getDataFromServer(modes.JobTitleList);
        $("#AdditionalTask").css("display", "block");
    }
    
    function closeModal()
    {
        $("header, footer, main").css("opacity", "1");
        $("#AdditionalTask").css("display", "none");
    }
    
    function checkFlag()
    {
        $("#ErrorMessage").text(!clickFlag ? "please,select an entry!" : "");
        return clickFlag;
    }
    
    function cutDate(date)
    {
        return date.substr(0, 10);
    }
    
    function selectPosition()
    {
        $("#JobTitleChoice option").each((index, option)=>
        {
            option.selected = (option.text === String(employeePosition)) ? "selected" : "";
        });
    }
    
    function checkData() 
    {
        let status = true;
        const Date = $("#BirthDate").val(),
        errorHandle = (selector)=>{
            $(selector).text(errorText);
            status = false;
        };
        
        if($("#Name").val().match(regularExpressions.NameCheck) == null)
        {
            errorHandle("label[for='Name']");
        }
        else if(Date.match(regularExpressions.DateCheck) == null)
        {
            errorHandle("label[for='BirthDate']");
        }
        else if($("#Phone").val().match(regularExpressions.PhoneNumberCheck) == null)
        {
            errorHandle("label[for='Phone']")
        }
        else if($("#JobTitleChoice").val() == null)
        {
            errorHandle("label[for='JobTitleChoice']")
        }
        return status;
        
    }
    
    
});