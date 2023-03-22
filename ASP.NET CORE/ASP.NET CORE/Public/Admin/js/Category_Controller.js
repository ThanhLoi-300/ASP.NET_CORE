var category_Controller = {
    init: function () {
        category_Controller.registerEvent()
        category_Controller.loadData();
    },
    registerEvent: function () {
        
    },
    loadData: function () {
        $.ajax({
            url: 'Category_List_1',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.rs == true) {
                    alert("ok")
                }
            }
        })
    }
}

category_Controller.init()