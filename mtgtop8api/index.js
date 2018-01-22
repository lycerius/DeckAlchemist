var express = require('express');
var app = express();

//APIs
var mtgtop8v1 = require('./api/mtgtop8/v1');

//Routers
app.use('/v1', mtgtop8v1);

//Start
app.listen(80, () => console.log("MTGTop8Api Started"));

