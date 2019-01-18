var TAFFY = require('taffy');


module.exports = function database(){
    var menu = TAFFY([
        {name : "Kotlet schabowy", price: "15.24", desc:"Tradycyjny pyszny polski schabowy. Poczuj magię domowego niedzielnego obiadu", available : "True", category : "Dania"},
        {name : "Pierogi ruskie", price:"8.16", desc:"Co tu dużo pisać...", available : "True", category : "Dania"},
        {name : "Kopytka", price:"20.16", desc:"Jak u Babci", available : "False", category : "Dania"},
        {name : "Naleśniki z twarogiem", price:"25.16", desc:"Cofnij się do przedszkola", available : "True", category : "Dania"},
        {name : "Pepsi", price:"4", available : "True", category: "Napoje"},
        {name : "Herbata", price:"4.20", available : "True", category: "Napoje"}
    ]);

    var categories = TAFFY([
        {name : "Dania"},
        {name : "Napoje"},
        {name : "Przystawki"}
    ]);

    var orders = TAFFY([

    ]);

    return{
        menu:menu,
        orders:orders,
        categories:categories
    };
}