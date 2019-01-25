var TAFFY = require('taffy');


module.exports = function database(){
    var menu = TAFFY([
        {name : "Kotlet schabowy", price: "15.24", desc:"Tradycyjny pyszny polski schabowy. Poczuj magię domowego niedzielnego obiadu", available : "True", category : "Dania", configurable:"True"},
        {name : "Pierogi ruskie", price:"8.16", desc:"Co tu dużo pisać...", available : "True", category : "Dania", configurable:"True"},
        {name : "Kopytka", price:"20.16", desc:"Jak u Babci", available : "False", category : "Dania", configurable:"True"},
        {name : "Naleśniki z twarogiem", price:"25.16", desc:"Cofnij się do przedszkola", available : "True", category : "Dania", configurable:"True"},
        {name : "Pepsi", price:"4", available : "True", category: "Napoje", configurable:"False"},
        {name : "Herbata", price:"4.20", available : "True", category: "Napoje", configurable:"False"}
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