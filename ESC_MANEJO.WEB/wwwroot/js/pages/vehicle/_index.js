initDT("dtVehicles");


async function Edit(id) {
    const url = await http('/Vehicle/SetIdEdit', 'POST', id, '/Home/Error/500');
    location.href = url;

}

async function Delete(id, status) {

    if (status === 'O') {
        await showIziToastError('No puedes eliminar un vehiculo ocupado', 'bottomCenter');
        return;
    }
    iziToast.show({
        theme: 'light',
        color: 'red',
        icon: 'icon-person',
        title: 'Hey',
        message: 'Esta seguro de eliminar el vehiculo?',
        position: 'center',
        progressBarColor: 'red',
        buttons: [
            ['<button>Si</button>', async function (instance, toast) {
                instance.hide({
                    transitionOut: 'fadeOutUp'
                }, toast, 'buttonName');
                const response = await http('/Vehicle/DeleteVehicle', 'POST', id, '/Home/Error/500');
                if (response.code === 0) {
                    await showIziToastSuccess('bottomCenter', '/Vehicle');
                } else {
                    await showIziToastError(response.description, 'bottomCenter');
                }
            }, true], // true to focus
            ['<button>No</button>', function (instance, toast) {
                instance.hide({
                    transitionOut: 'fadeOutUp'
                }, toast, 'buttonName');
            }]
        ]
    });
}