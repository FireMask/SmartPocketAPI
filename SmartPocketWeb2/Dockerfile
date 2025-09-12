FROM node:current-alpine AS build
WORKDIR /SmartPocketWeb2
COPY package*.json ./
RUN npm ci
COPY . .
RUN npm run build

FROM nginx:alpine
COPY ./nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /SmartPocketWeb2/dist/SmartPocketWeb2/browser /usr/share/nginx/html
EXPOSE 80
