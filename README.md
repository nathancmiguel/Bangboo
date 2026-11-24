# Bangboo Discord Bot

A Discord bot built with C# and [NetCord](https://netcord.dev), hosted on [Discloud](https://discloud.com).

Hi, I’m @NathanMiguel, and I’m developing a bot to help me moderate my community and streamline some tasks.

This project is currently for my personal use, but I hope to monetize it in the future. 😄

## Features

- `/moderate`: delete messages, timeout, kick, ban and unban members through modals (staff only)
- `/discloud`: show the bot status or logs from the host, with a refresh button (staff only)
- `/profile`: show when your (or another member's) account was created
- `/fab` and `/social`: links to my Fab store and social media
- Welcome, goodbye, boost and ban cards drawn with SkiaSharp
- Booster role kept in sync with server boosts
- Mousetrap: anyone who posts in the trap channel is banned (long-time members are unbanned and warned by DM)

## Download and Install

---

- [Git](https://git-scm.com)
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (optional, to test the image used on Discloud)

## Configuration

Copy `Bot/appsettings.json.example` to `Bot/appsettings.json` and fill in:

```json
{
    "Discord": { "Token": "your bot token" },
    "Discloud": { "Token": "your Discloud API token", "AppId": "your Discloud app id" }
}
```

`appsettings.json` is ignored by git. The same values can also come from environment variables
(`Discord__Token`, `Discloud__Token`, `Discloud__AppId`).

Server data lives in `Bot/Data`:

- `guild.json`: announcement and mousetrap channels, staff (`kernel`, `stf`) and booster roles
- `colors.json`, `emojis.json`, `fab.json`, `social.json`: embed colors, emoji IDs and links

Images and fonts used by the cards live in `Bot/Assets`. Command translations live in `Bot/Localizations`.

## Scripts

- `dotnet run --project Bot`: run the bot
- `dotnet build`: build the project
- `docker build -t bangboo .`: build the Docker image used on Discloud

## Deploy on Discloud

The app runs from the `Dockerfile` (`MAIN=Dockerfile` in `discloud.config`). Upload the repository root,
including `Bot/appsettings.json`; `.discloudignore` leaves out build output, the Figma file and the icon pack.

## Thirdparty Libraries

- [NetCord](https://netcord.dev)
- [Discloud.Net](https://github.com/nathancmiguel/Discloud.NET)
- [SkiaSharp](https://github.com/mono/SkiaSharp)
- [Newtonsoft.Json](https://www.newtonsoft.com/json)
