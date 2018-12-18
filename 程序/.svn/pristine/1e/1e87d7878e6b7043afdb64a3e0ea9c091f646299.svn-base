var clients = [];
$(function () {
    clients = $.clientsInit();
})
$.clientsInit = function () {
    var dataJson = {
        dataItems: [],
        authorizeMenu: []
        
    };
    var init = function () {
        $.ajax({
            url: "/Main/GetClientsDataJson",
            type: "get",
            dataType: "json",
            async: false,
            success: function (data) {
                dataJson.dataItems = data.dataItems;
                dataJson.authorizeMenu = eval(data.authorizeMenu);
            }
        });
    }
    init();
    return dataJson;
}