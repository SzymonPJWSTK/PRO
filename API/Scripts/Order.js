module.exports  = function(database){

    function placeOrder(json){
        var order = [];
        for (var key in json) {
            if (json.hasOwnProperty(key)) {
                var orderedProduct = database.menu({___id:key}).get();

                var orderItem = {
                    name:  orderedProduct[0].name,
                    quantity: json[key],
                    id: key
                }

                order.push(orderItem);
            }
        }

        database.orders.insert({ zamówienie: order});
    }

    function removeOrder(order){
        database.orders({___id:order.id}).remove();
    }

    return{
        placeOrder:placeOrder,
        removeOrder: removeOrder
    };

};