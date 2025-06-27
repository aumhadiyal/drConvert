using System.Data;
using drConverter;

DataTable dataTable = new DataTable();
dataTable.Columns.Add("EmployeeId", typeof(int));
dataTable.Columns.Add("FullName", typeof(string));
dataTable.Columns.Add("Department", typeof(string));
dataTable.Columns.Add("JoinDate", typeof(DateTime));
dataTable.Columns.Add("IsActive", typeof(bool));

DataRow dataRow = dataTable.NewRow();
dataRow["EmployeeId"] = 101;
dataRow["FullName"] = "Jane Smith";
dataRow["Department"] = "HR";
dataRow["JoinDate"] = new DateTime(2020, 3, 15);
dataRow["IsActive"] = true;

Employee e = DataRowConverter.Convert<Employee>(dataRow);

Console.WriteLine(
    $@"EmployeeId: {e.EmployeeId}, 
    FullName: {e.FullName}, 
    Department: {e.Department}, 
    JoinDate: {e.JoinDate}, 
    IsActive: {e.IsActive} "
);
