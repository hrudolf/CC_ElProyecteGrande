FROM nginx
COPY build /usr/share/nginx/html
RUN mv /etc/nginx/nginx.conf /etc/nginx/nginx.conf_orig 
RUN cp nginx.conf /etc/nginx/nginx.conf