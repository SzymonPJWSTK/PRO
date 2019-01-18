module.exports = function(database){

    function availability(json){
        database.menu({___id:json.id}).update({available:json.isOn})
    };

    return{
        availability, availability
    }
}