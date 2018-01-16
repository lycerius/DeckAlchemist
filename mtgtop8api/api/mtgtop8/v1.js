var express = require('express')
var top8 = require('mtgtop8')
var router = express.Router()

function sendResponse(responseStream, error, data) {
    if(error) responseStream.status(404).send()
    else responseStream.send(data)
}

router.use((req, res, next) => {
    next()
})

router.get('/ping', (req, res) => {
    res.status(200).send()
})

router.get('/events/standard/:page', (req, res) => {
    var page = req.params['page']
    top8.standardEvents(page, (err, result) => {
        sendResponse(res, err, result)
    })
})

router.get('/events/modern/:page', (req, res) => {
    var page = req.params['page']
    top8.modernEvents((err, events) => {
        sendResponse(res, err, events)
    })
})

router.get('events/info/:eventId', function(req, res) {
    var eventId = req.params['eventId']
    top8.eventInfo(eventId, (err, result) => {
        sendResponse(res, err, result)
    })
})

router.get('events/info/:eventId/more', function(req, res) {
    var eventId = req.params['eventId']
    top8.event(eventId, (err, result) => {
        sendResponse(res, err, result)
    })
})

router.get('deck/:eventId/:deckId', function(req, res) {
    var eventId = req.params['eventId']
    var deckId = req.params['deckId']
    top8.deck(eventId, deckId, (err, result) => {
        sendResponse(res, err, result)
    })
})


module.exports = router

