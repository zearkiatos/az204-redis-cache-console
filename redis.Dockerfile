FROM redis:8.2-alpine

COPY ./docker/redis/redis.conf /usr/local/etc/redis/redis.conf
COPY ./docker/redis/users.acl /usr/local/etc/redis/users.acl

RUN chmod 644 /usr/local/etc/redis/redis.conf \
    && chmod 600 /usr/local/etc/redis/users.acl

CMD ["redis-server", "/usr/local/etc/redis/redis.conf"]