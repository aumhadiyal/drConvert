using System.Data;
using drConverter;

DataTable dataTable = new DataTable();
dataTable.Columns.Add("EmployeeId", typeof(int));
dataTable.Columns.Add("FullName", typeof(string));
dataTable.Columns.Add("Department", typeof(string));
dataTable.Columns.Add("JoinDate", typeof(DateTime));
dataTable.Columns.Add("IsActive", typeof(bool));

DataRow dataRow1 = dataTable.NewRow();
dataRow1["EmployeeId"] = 101;
dataRow1["FullName"] = "Jane Smith";
dataRow1["Department"] = "HR";
dataRow1["JoinDate"] = new DateTime(2020, 3, 15);
dataRow1["IsActive"] = true;
dataTable.Rows.Add(dataRow1);

DataRow dataRow2 = dataTable.NewRow();
dataRow2["EmployeeId"] = 102;
dataRow2["FullName"] = "John Doe";
dataRow2["Department"] = "IT";
dataRow2["JoinDate"] = new DateTime(2019, 7, 10);
dataRow2["IsActive"] = false;
dataTable.Rows.Add(dataRow2);

DataRow dataRow3 = dataTable.NewRow();
dataRow3["EmployeeId"] = 103;
dataRow3["FullName"] = "Alice Johnson";
dataRow3["Department"] = "Finance";
dataRow3["JoinDate"] = new DateTime(2021, 1, 5);
dataRow3["IsActive"] = true;
dataTable.Rows.Add(dataRow3);

DataRow dataRow4 = dataTable.NewRow();
dataRow4["EmployeeId"] = 104;
dataRow4["FullName"] = "Bob Lee";
dataRow4["Department"] = "Marketing";
dataRow4["JoinDate"] = new DateTime(2018, 11, 20);
dataRow4["IsActive"] = false;
dataTable.Rows.Add(dataRow4);

DataRow dataRow5 = dataTable.NewRow();
dataRow5["EmployeeId"] = 105;
dataRow5["FullName"] = "Sara Kim";
dataRow5["Department"] = "Sales";
dataRow5["JoinDate"] = new DateTime(2022, 6, 1);
dataRow5["IsActive"] = true;
dataTable.Rows.Add(dataRow5);

DataRow dataRow = dataTable.Rows[0];

Employee e = DataRowConverter.Convert<Employee>(dataRow);

Console.WriteLine(
    $@"EmployeeId: {e.EmployeeId}, 
    FullName: {e.FullName}, 
    Department: {e.Department}, 
    JoinDate: {e.JoinDate}, 
    IsActive: {e.IsActive} "
);

List<Employee> employees = DataRowConverter.ConvertList<Employee>(dataTable);
foreach (var emp in employees)
{
    Console.WriteLine(
        $@"EmployeeId: {emp.EmployeeId}, 
        FullName: {emp.FullName}, 
        Department: {emp.Department}, 
        JoinDate: {emp.JoinDate}, 
        IsActive: {emp.IsActive} "
    );
}
