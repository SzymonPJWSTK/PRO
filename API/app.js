//#region VARIBLES
const express = require('express');
const bodyParser = require('body-parser');
const WebSocket = require('ws');
const wss = new WebSocket.Server({port:8081}); 

var app = express();
var database = require('./db/database')();
var basket = require('./Scripts/Basket')(database);
var order = require('./Scripts/Order')(database);
var menu = require('./Scripts/Menu')(database);
var kitchenWss;
//#endregion

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({extended: true}));

app.get('/categories', function(req,res){
    var response = database.categories().get();
    res.status(200).json(response);
});

//#region  MENU
app.get('/menu', function (req,res) {
   var response = database.menu({category: req.query.category, available : "True"}).get();
   res.status(200).json(response);
});

app.get('/menu/all', function(req,res){
    var response = database.menu().get();
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

app.post('/menu/availability', function(req,res){
    menu.availability(req.body);
    res.status(200);
});
//#endregion

//#region  BASKET
app.post('/basket/products', function(req,res){
    res.status(200).json(basket.products(req.body));
});

app.post('/basket/recalculate', function(req,res){
    res.status(200).json(basket.recalcuate(req.body));
});
//#endregion

//#region ORDER
app.post('/order/place', function(req,res){
    order.placeOrder(req.body);
    if(kitchenWss != undefined)
        kitchenWss.send(JSON.stringify(database.orders().last()));

    res.status(200).json();
});

app.post('/order/remove', function(req,res){
    order.removeOrder(req.body);
    res.status(200).json();
});

//#endregion

//#region  WEBSOCKET
wss.on('connection',ws=>{
    kitchenWss = ws;
});
//#endregion

const PORT = process.env.PORT || 8080;
app.listen(PORT, () => {
  console.log(`App listening on port ${PORT}`);
  console.log('Press Ctrl+C to quit.');
});