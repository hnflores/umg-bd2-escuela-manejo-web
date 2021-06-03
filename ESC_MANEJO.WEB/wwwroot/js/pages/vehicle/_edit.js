(function () {
    'use strict';
    window.addEventListener('load', function () {
        var forms = document.getElementsByClassName('needs-validation');
        Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', async function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                } else {
                    event.preventDefault();
                    await UpdateVehicle();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();

async function UpdateVehicle() {
    const request = {
        token: document.getElementsByName('__RequestVerificationToken')[0].value,
        VehicleId: document.getElementById('placa').value,
        Marca: document.getElementById('marca').value,
        Modelo: document.getElementById('modelo').value,
        Anio: document.getElementById('anio').value,
        FechaCompra: document.getElementById('fecha-compra').value,
        Estado: document.getElementById('estado').value,
    }
    await showButtonLoading('btnUpdate');
    const response = await http('/Vehicle/UpdateVehicle', 'POST', request, '/Home/Error/500', request.token);
    if (response.code === 0) {
        await showIziToastSuccess('bottomCenter', '/Vehicle');        
    } else {
        await showIziToastError(response.description, 'bottomCenter');
    }
    await hideButtonLoading('btnUpdate');
}
