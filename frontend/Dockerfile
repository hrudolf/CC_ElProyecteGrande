FROM nginx
COPY build /usr/share/nginx/html
RUN rm /etc/nginx/conf.d/default.conf
RUN cp conf /etc/nginx/