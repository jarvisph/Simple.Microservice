SET NAME=Simple.Utils
SET IMAGES=webutils
SET ACR=registry.cn-shenzhen.aliyuncs.com
cd bin\Debug\net6.0
echo Create Dockerfile File
del Dockerfile
echo FROM mcr.microsoft.com/dotnet/aspnet:6.0  >> Dockerfile
echo WORKDIR /app  >> Dockerfile
echo COPY . .  >> Dockerfile
echo RUN ln -s /lib/x86_64-linux-gnu/libdl-2.31.so /usr/lib/libdl.so >> Dockerfile
echo ENTRYPOINT ["dotnet", "%NAME%.dll"] >> Dockerfile
docker rmi %ACR%/simplehub/%IMAGES%
docker rmi %IMAGES%
docker build -t %IMAGES% .
docker tag %IMAGES% %ACR%/simplehub/%IMAGES%
docker push %ACR%/simplehub/%IMAGES%