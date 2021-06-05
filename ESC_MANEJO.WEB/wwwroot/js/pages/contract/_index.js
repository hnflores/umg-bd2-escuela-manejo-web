initDT("dtContracts");

async function Edit(id) {
    const url = await http('/Contract/SetIdEdit', 'POST', id, '/Home/Error/500');
    location.href = url;
}

async function Delete(id) {
    iziToast.show({
        theme: 'light',
        color: 'red',
        icon: 'icon-person',
        title: 'Hey',
        message: 'Esta seguro de eliminar el contrato?',
        position: 'center',
        progressBarColor: 'red',
        buttons: [
            ['<button>Si</button>', async function (instance, toast) {
                instance.hide({
                    transitionOut: 'fadeOutUp'
                }, toast, 'buttonName');
                const response = await http('/Contract/DeleteContract', 'POST', id, '/Home/Error/500');
                if (response.code === 0) {
                    await showIziToastSuccess('bottomCenter', '/Contract');
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