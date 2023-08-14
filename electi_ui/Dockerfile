# Use the official Ubuntu base image
FROM node:latest

RUN mkdir -p /etc/electi_UI 
WORKDIR /etc/electi_UI

COPY . .

RUN yarn install 
RUN yarn build
EXPOSE 3000

# Start the app using the ENTRYPOINT
CMD ["yarn", "start"]