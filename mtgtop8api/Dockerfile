FROM node:carbon

ENV port 3498

WORKDIR /usr/src/app

COPY package*.json ./

RUN npm install

COPY . .

EXPOSE ${port}

CMD ["npm", "start"]