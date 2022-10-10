$(document).ready(function () {
	alert('ok');
});

$(document).ready(function () {

	GetCustomer();
});

function GetCustomer() {

	$.ajax({
		url: '/Table/GetEmployeeList',
		type: 'Get',
		dataType: 'json',
		success: OnSuccess
	})
}

function OnSuccess(response) {

	$('#DataTableData').DataTable({
		bProcessing: true,
		bLenghtChange: true,
		lenghtMenu: [[5, 10, 25, -1], [5, 10, 25, "All"]],
		bFilter: true,
		bSort: true,
		bPaginate: true,
		data: response,
		columns: [
			{
				data: 'id',
				render: function (data, type, row, meta) {
					return row.ID
				}

			}, 

			{
				data: 'firstname',
				render: function (data, type, row, meta) {
					return row.FirstName
				}

			}, 

			{
				data: 'lastname',
				render: function (data, type, row, meta) {
					return row.LastName
				}

			}, 

			{
				data: 'gender',
				render: function (data, type, row, meta) {
					return row.Gender
				}

			}, 

			{
				data: 'country',
				render: function (data, type, row, meta) {
					return row.Country
				}

			}, 

			{
				data: 'age',
				render: function (data, type, row, meta) {
					return row.Age
				}

			}, 

			

			
		]
	});
}



