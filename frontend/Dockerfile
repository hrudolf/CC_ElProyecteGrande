FROM nginx
COPY build /usr/share/nginx/html
RUN rm /etc/nginx/conf.d/default.conf
RUN cp ./nginx.conf /etc/nginx/conf.d/default.conf