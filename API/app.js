const express = require('express');
const bodyParser = require('body-parser');

var app = express();
var database = require('./db/database')();
var basket = require('./Scripts/Basket')(database);

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({extended: true}));

app.get('/categories', function(req,res){
    var response = database.categories().get();
    res.status(200).json(response);
});

//#region  MENU
app.get('/menu', function (req,res) {
   var response = database.menu({category: req.query.category}).get();
   res.status(200).json(response);
});

app.post('/menu', function(req,res){
    var meal = {
                name:req.body.name, 
                category:req.body.name
               };
    database.menu.insert(meal);
    res.status(200).send("ok");
});

app.get('/menu/products', function(req,res){
    var response = database.menu({___id:req.query.id}).get();
    res.status(200).json(response);
});
//#endregion

//#region  BASKET
app.post('/basket/products', function(req,res){
    res.status(200).json(basket.products(req.body));
});

app.post('/basket/recalculate', function(req,res){
    res.status(200).json(basket.recalcuate(req.body));
});

app.post('/basket/place', function(req,res){
    basket.placeOrder(req.body);
    res.status(200).json();
});
//#endregion

const PORT = process.env.PORT || 8080;
app.listen(PORT, () => {
  console.log(`App listening on port ${PORT}`);
  console.log('Press Ctrl+C to quit.');
});