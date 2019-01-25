module.exports = function(database){

    function addMenu(json)
    {
        var categoryName = database.categories({___id:json.category}).get();
        console.log(json);
        var meal = {
            name : json.mealName,
            price: json.mealPrice,
            desc: json.mealDesc,
            available : "False",
            category : categoryName[0].name

        }

        database.menu.insert(meal);
    };

    function addCategory(json){
        var categoryName = String(json.categoryName);
        database.categories.insert({name: categoryName});
    };

    function availability(json){
        database.menu({___id:json.id}).update({available:json.isOn})
    };

    return{
        availability, availability,
        addCategory:addCategory,
        addMenu: addMenu
    }
}