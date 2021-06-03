initDT("dtVehicles");

async function Edit(id) {
    const url = await http('/Vehicle/SetIdEdit', 'POST', id, '/Home/Error/500');
    location.href = url;

}