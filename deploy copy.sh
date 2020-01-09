docker build -t stack-overflow-clone-cg-image .

docker tag stack-overflow-clone-cg-image registry.heroku.com/stack-overflow-clone-cg/web

docker push registry.heroku.com/stack-overflow-clone-cg/web

heroku container:release web -a stack-overflow-clone-cg