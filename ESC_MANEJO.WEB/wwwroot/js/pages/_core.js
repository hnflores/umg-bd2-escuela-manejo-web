async function http(url, method, params, urlError, xToken = '') {
    let response;
    try {
        const res = await fetch(url, {
            method,
            headers: {
                'Accept': 'application/json, text/plain, */*',
                'Content-Type': 'application/json',
                'RequestVerificationToken': xToken
            },
            body: JSON.stringify(params)
        });
        response = await res.json();
    } catch (e) {
        console.log(e);
        location.href = urlError;
    }
    return response;
}
function initDT(id, order = 1, orderType = 'desc') {
    return $('#' + id).DataTable({
        language: {
            url: '../lib/datatable/spanish.json'
        },
        order: [[order, orderType]]
    });
}

async function showButtonLoading(buttonId) {
    let button = document.getElementById(buttonId);
    let load = button.getElementsByTagName('div')[0];
    let body = button.getElementsByTagName('div')[1];
    load.style.display = 'block';
    body.style.display = 'none';
    button.toggleAttribute('disabled', true);
}
async function hideButtonLoading(buttonId) {
    let button = document.getElementById(buttonId);
    let load = button.getElementsByTagName('div')[0];
    let body = button.getElementsByTagName('div')[1];
    load.style.display = 'none';
    body.style.display = 'block';
    button.toggleAttribute('disabled', false);
}
async function showIziToastSuccess(position, urlRedirect = '') {
    iziToast.show({
        title: 'Excelente!',
        message: 'Proceso realizado correctamente.',
        color: 'green',
        position,
        timeout: 2500,
        onClosed: function () {
            if (urlRedirect.length > 0)
                location.href = urlRedirect;
        }
    });
}
async function showIziToastError(message, position) {
    iziToast.show({
        title: 'Advertencia!',
        message,
        color: 'red',
        position,
        timeout: 2500
    });
}