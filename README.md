# Deployment
This package requires the Segoe UI font to function properly. You can acquire this font by using a Windows machine and copying it from the directory `C:\Windows\Fonts\segoeui.ttf` into the `Resources/Fonts` directory.<br>
Run the following commands to build and run the docker container<br>
```
docker build -t AchievmentAPI .
docker run -p {port}:80 AchievementAPI
```
{port} can be any port available on the host system for the container to receive the requests through.
## Security notice
This API does not use HTTPS, so you will need a third party provider to enable SSL communication

# Usage
There is one endpoint in this api, the `Achievement/api` endpoint. This endpoint can be sent a `GET` request to get an achievement image correlating to the sent JSON body, which follows the format:
```
{
    "achievementType": int,
    "achievementName": string
    "gamerScore": long
}
```
achievementType has three options:
```
1: Xbox 360
2: Xbox One
3: Xbox One Rare
```
Any other input besides these three will result in a 400 response code<br><br>
achivementName can be any string greater than 0 characters. The image processing does not wrap text, so it may overflow outside the base image.<br><br>
gamerScore can be any number greater than 0. If 0 or less is passed, a random gamerscore will be chosen