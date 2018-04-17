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

## How to:
### Import from a csv
On the My Collection page, there is a button "Import From CSV", click it and select the file from your computer.
### Search for Decks
Click the Decks tab and the deck builder should show up, populated with various decks to build.
### Make a group
Click the Groups tab and click the button +New Group
### Invite a person to my group
Next to the group name, click +Invite and then enter the username of the person you want to invite.
### Enter IRC
Click the group and the large window on the right will start an in-browser IRC client for all users of the group. 
Other users must enter the IRC themselves.
### Respond to a group invite
Click on the Inbox tab to see any recent invites.
### View Meta Analysis
Click on the Meta Analytics Tab
### Add individual cards to the collection
On the My Collection page, click Add Cards then enter in the name or partial name of the card(s) you want to add.
### Search for a card
On the My Collection page, click Add Cards then enter in the name or partial name of the card(s).
### Mark a card as lendable/not lendable
On the My Collection page, select the checkbox next to the cards you want to toggle, and click Toggle Lendable, this will switch non-lendable cards to lendable and vice versa.
### Send private messages to group memebers
On the Groups tab, click the user's name to send them a message
### Send a loan request
On the Groups tab, click the user's name and change the message type to loan request, then select the cards that you would like to request.
### Respond to a loan request
Click the Inbox tab to see any recent loan requests and respond to them accordingly.
### View card images
On the My Collection tab, mouse over any card to see its image on the right.
