//$(document).ready(function () {
//	alert('ok');
//});

$(document).ready(function () {

	GetCustomer();
});

function GetCustomer() {

	$.ajax({
		url: '/Table/GetCustomerList',
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
				data: 'ID',
				render: function (data, type, row, meta) {
					return row.id
				}

			}, 

			{
				data: 'FIRSTNAME',
				render: function (data, type, row, meta) {
					return row.firstname
				}

			}, 

			{
				data: 'LASTNAME',
				render: function (data, type, row, meta) {
					return row.lastname
				}

			}, 

			{
				data: 'GENDER',
				render: function (data, type, row, meta) {
					return row.gender
				}

			}, 

			{
				data: 'COUNTRY',
				render: function (data, type, row, meta) {
					return row.country
				}

			}, 

			{
				data: 'AGE',
				render: function (data, type, row, meta) {
					return row.age
				}
			}, 	
		]
	});
}



