const express = require('express');

var app = express();

app.get('/getMeals', function (req,res) {
    res.status(200).send("Operacja wykonana pomyślnie - brak dań");
});

app.post('/addMeals', function(req,res){
    res.status(200).send("Operacja wykonana pomyślnie - brak funkcji bazy danych")
});

const PORT = process.env.PORT || 8080;
app.listen(PORT, () => {
  console.log(`App listening on port ${PORT}`);
  console.log('Press Ctrl+C to quit.');
});