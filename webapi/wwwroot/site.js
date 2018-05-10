const uri = 'api/contacts';
let contacts = null;
function getCount(data) {
    const el = $('#counter');
    let name = 'contact';
    if (data) {
        if (data > 1) {
            name = 'contacts';
        }
        el.text(data + ' ' + name);
    } else {
        el.html('No ' + name);
    }
}

$(document).ready(function () {
    getData();
});

function getData() {
    $.ajax({
        type: 'GET',
        url: uri,
        success: function (data) {
            $('#contacts').empty();
            getCount(data.length);
            $.each(data, function (key, item) {
               

                $('<tr>' +
                    '<td>' + item.firstName + '</td>' +
                    '<td>' + item.lastName + '</td>' +
                    '<td>' + item.email + '</td>' +
                    '<td>' + item.message + '</td>' +
                    '<td><button onclick="editContact(' + item.contactId + ')">Edit</button></td>' +
                    '<td><button onclick="deleteContact(' + item.contactId + ')">Delete</button></td>' +
                    '</tr>').appendTo($('#contacts'));
            });

            contacts = data;
        }
    });
}

function addContact() {
    const contact = {
        'firstName': $('#add-firstname').val(),
        'lastName': $('#add-lastname').val(),
        'email': $('#add-email').val(),
        'message': $('#add-message').val(),
    };

    $.ajax({
        type: 'POST',
        accepts: 'application/json',
        url: uri,
        contentType: 'application/json',
        data: JSON.stringify(contact),
        error: function (jqXHR, textStatus, errorThrown) {
            alert('here');
        },
        success: function (result) {
            getData();
            $('#add-firstname').val('');
            $('#add-lastname').val('');
            $('#add-email').val('');
            $('#add-message').val('');
        }
    });
}

function deleteContact(id) {
    $.ajax({
        url: uri + '/' + id,
        type: 'DELETE',
        success: function (result) {
            getData();
        }
    });
}

function editContact(id) {
    $.each(contacts, function (key, item) {
        if (item.contactId === id) {
            $('#edit-firstname').val(item.firstName);
            $('#edit-lastname').val(item.lastName);
            $('#edit-email').val(item.email);
            $('#edit-contactId').val(item.contactId);
            $('#edit-message').val(item.message);
        }
    });
    $('#spoiler').css({ 'display': 'block' });
}

$('.my-form').on('submit', function () {
    const contact = {
        'firstName': $('#edit-firstname').val(),
        'lastName': $('#edit-lastname').val(),
        'email': $('#edit-email').val(),
        'message': $('#edit-message').val(),
        'contactId': $('#edit-contactId').val()
    };

    $.ajax({
        url: uri + '/' + $('#edit-contactId').val(),
        type: 'PUT',
        accepts: 'application/json',
        contentType: 'application/json',
        data: JSON.stringify(contact),
        success: function (result) {
            getData();
        }
    });

    closeInput();
    return false;
});

function closeInput() {
    $('#spoiler').css({ 'display': 'none' });
}