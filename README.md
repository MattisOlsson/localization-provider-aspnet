# LocalizationProvider for Asp.Net

Database driven localization provider for .Net Core applications.

[<img src="https://tech-fellow-consulting.visualstudio.com/_apis/public/build/definitions/70e95aed-5f16-4125-b7bb-60aeea07539d/10/badge"/>](https://tech-fellow-consulting.visualstudio.com/localization-provider-aspnet/_build/index?definitionId=10)

## What is the LocalizationProvider project?

LocalizationProvider project is Asp.Net Mvc web application localization provider on steriods.

Giving you main following features:
* Database driven localization provider for .Net applications
* Easy resource registrations via code
* Supports hierarchical resource organization (with help of child classes)

## Project Structure

Database localization provider is split into main [abstraction projects](https://github.com/valdisiljuconoks/LocalizationProvider) and Asp.Net support project (this).

## Getting Started

* [Getting Started](docs/getting-started-net.md)

## GitHub Source Code Structure

Asp.Net support project has its own repo while main abstraction projects are included as [submodules](https://gist.github.com/gitaarik/8735255) here.

# How to Contribute

It's super cool if you read this section and are interesed how to help the library. Forking and playing around sample application is the fastest way to understand how localization provider is working and how to get started.

Forking and cloning repo is first step you do. Keep in mind that provider is split into couple repositories to keep thigns separated. Additional repos are pulled in as submodules. If you Git client does not support automatic checkout of the submodules, just execute this command at the root of the checkout directory:

```
git clone --recurse-submodules git://github.com/...
```

# More Info

* [Part 1: Resources and Models](http://blog.tech-fellow.net/2016/03/16/db-localization-provider-part-1-resources-and-models/)
* [Part 2: Configuration and Extensions](http://blog.tech-fellow.net/2016/04/21/db-localization-provider-part-2-configuration-and-extensions/)
* [Part 3: Import and Export](http://blog.tech-fellow.net/2017/02/22/localization-provider-import-and-export-merge/)
* [Part 4: Resource Refactoring and Migrations](https://blog.tech-fellow.net/2017/10/10/localizationprovider-tree-view-export-and-migrations/)
