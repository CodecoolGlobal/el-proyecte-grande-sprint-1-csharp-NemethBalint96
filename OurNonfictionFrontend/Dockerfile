FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs

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