var express = require('express');
var app = express();

//APIs
var mtgtop8v1 = require('./api/mtgtop8/v1');

//Routers
app.use('/v1', mtgtop8v1);

var port = process.env.Port ? process.env.Port : 3498;

console.log("Using port "+port);

//Start
app.listen(port, () => console.log("MTGTop8Api Started"));

