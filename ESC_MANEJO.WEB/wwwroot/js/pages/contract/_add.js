
let dtSesion;

(function () {
    'use strict';

    $('.select2-container').select2();
    dtSesion = initDT("tblSesion");

    window.addEventListener('load', function () {
        var forms = document.getElementsByClassName('needs-validation');
        Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', async function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                } else {
                    event.preventDefault();
                    if (form.id === 'saveContract') {
                        await AddContract();
                    } else if (form.id == 'addSession') {
                        AddSession();
                    }
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();

function AddSession() {

    let fechaInicio = document.querySelector("#sesion-fecha-inicio");
    let fechaFin = document.querySelector("#sesion-fecha-fin");

    dtSesion.row.add([
        fechaInicio.value,
        fechaFin.value,
        '<button type="button" class="btn btn-danger">Eliminar</button>'
    ]).draw(false);

    $("#mdlAddSesion").modal('hide');
    fechaInicio.value = '';
    fechaFin.value = '';
}

$('#tblSesion tbody').on('click', 'button', function () {
    dtSesion.row($(this).parents('tr')).remove().draw();
});

async function AddContract() {
    let sesiones = Array();

    const rowSession = dtSesion.rows().data();
    if (rowSession.length === 0) {
        await showIziToastError('Se debe agregar al menos una sesión', 'bottomCenter');
        return;
    }
    for (let i = 0; i < rowSession.length; i++) {
        const item = rowSession[i];
        sesiones.push({
            FechaInicio: item[0],
            FechaFin: item[1]
        });
    }

    const request = {
        token: document.getElementsByName('__RequestVerificationToken')[0].value,
        CustomerId: document.getElementById('customer').value,
        UserId: parseInt(document.getElementById('driver').value),
        VehicleId: document.getElementById('vehicle').value,
        Costo: parseFloat(document.getElementById('costo').value),
        NumeroSesiones: sesiones.length,
        FechaInicio: document.getElementById('fecha-inicio').value,
        FechaFin: document.getElementById('fecha-fin').value,
        Sesiones: sesiones
    }
    await showButtonLoading('btnAdd');
    const response = await http('/Contract/AddContract', 'POST', request, '/Home/Error/500', request.token);
    if (response.code === 0) {
        await showIziToastSuccess('bottomCenter', '/Vehicle');
    } else {
        await showIziToastError(response.description, 'bottomCenter');
    }
    await hideButtonLoading('btnAdd');
}