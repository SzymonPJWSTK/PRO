var TAFFY = require('taffy');

module.exports = function(database){

    function recalcuate(json){
        var cost = 0;
        for (var key in json) {
            if (json.hasOwnProperty(key)) {
                var price = database.menu({___id:key}).get();
                var totalPrice = Number(price[0].price) * Number(json[key]);
                cost += totalPrice;
            }
        }

        var response = {
            cost: cost
        };

        return response;
    }

    function products(json){

        var response = [];

        for (var key in json) {
            if (json.hasOwnProperty(key)) {
                var dbProduct = database.menu({___id:key}).get();
                var chosenProduct = {
                    name: dbProduct[0].name,
                    price: dbProduct[0].price,
                    ___id: key
                }
                response.push(chosenProduct);
            }
        }

        return response;
    }

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

    return{
        recalcuate:recalcuate,
        products:products,
        placeOrder: placeOrder
    };
}