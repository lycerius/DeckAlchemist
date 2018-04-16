# DeckAlchemist

Deck Alchemist aims to increase community engagement as well as mitigate the financial impact of playing CCG at a high level by allowing players to organize into groups to share cards and ideas while communicating as a team. 

This application will be free to access on the web. 

## What does it do

Players will be able to manage collections of cards for various card games as well as form groups for loaning cards that would otherwise be too expensive to afford

## Demo

A live version of DeckAlchemist can be viewed [here](http://209.6.196.14:81/)

## Building

To build/run a local instance, you must have [docker](https://github.com/docker) installed on your system.

To run a local instance must have a `.env` file in your local repo. The `.env` file can be used to set environment variables for docker. You must change these to work with your local environment. An example is provided in the repo named `.env.example`

Run `docker-compose build` to build all the docker containers required. To then launch a local instance of DeckAlchemist, run `docker-compose up`. You can then destory the local instance using `docker-compose down`
