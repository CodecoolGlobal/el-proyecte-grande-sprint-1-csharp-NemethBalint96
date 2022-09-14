FROM node

# set working directory
WORKDIR /app

#COPY . .
# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install app dependencies
COPY OurNonfictionFrontend/package.json /app/package.json
COPY OurNonfictionFrontend/package-lock.json /app/package-lock.json
RUN npm install --force
RUN npm install react-scripts -g --force

# add app
COPY OurNonfictionFrontend/. ./

EXPOSE 3000

# start app
CMD ["npm", "start"]