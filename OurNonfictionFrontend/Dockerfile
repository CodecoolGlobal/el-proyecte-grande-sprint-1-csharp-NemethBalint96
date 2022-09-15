FROM microsoft/aspnetcore:2.0 AS base
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_6.x | bash - && \
    apt-get install -y build-essential nodejs

# set working directory
WORKDIR /app

#COPY . .
# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install app dependencies
COPY OurNonfictionFrontend/package.json /app/package.json
COPY OurNonfictionFrontend/package-lock.json /app/package-lock.json
RUN npm install --force

# add app
COPY OurNonfictionFrontend/. ./

# start app
CMD ["npm", "start"]