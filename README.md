# CrimeMapping.com Email Parser

## Table of Contents

* [Description](#description)
* [Installation](#installation)
* [Usage](#usage)
* [Contributions](#contributions)
* [License](#license)


## Description

Read email alerts that have been received from the CrimeMapping.com website.
This library is designed to read plain text emails that are sent from the site.

Crimemapping.com does not provide an API to retrive the information that is available
on their website. They do allow for users to subscribe to email alerts that are sent
from the website. When subscribimg to the email alerts, the same email address can
be used to receive mutliple alerts to the same email address.

This library allows you to parse those emails in your C# application and save them
in JSON format for use within your application.


## Installation 

Using the terminal, navigate to your project's application directory. Then run the 
commands below.

```bash
dotnet add package Almostengr.CrimeMappingCom.EmailParser
dotnet restore
```

The package will download and install.


## Usage

### appsettings.json

Add the below to your appsettings.json file.  This file is used to configure the 
library and its functions.

```json
{
    "CrimeMappingSettings": { 
        "Hostname": "mail.example.com",
        "Username" : "emailuser",
        "Password":  "emailpass",
        "PortNumber": 993,
        "OutputDirectory" : "/home/almostengr/crimedata",
    }
}
```

#### Hostname

The address of the email server that will be receiving the email alerts.

#### Username 

The username of the account that will be receiving the email alerts.

#### Password

The password of the email account that will be receiving the email alerts.

#### PortNumber

The port number of the server that should be used to connect to the email server. 

#### OutputDirectory

The location on the file system, that the json files will be written to for further use.


### With Dependency Injection

In the Program.cs file, add the below to include all services within this library.

```csharp
builder.Services.AddCrimeMappingServices(builder.Configuration);
```

In your service method or class, added the below code to read all of the unread emails.

```csharp
List<(MimeMessage, MailKit.UniqueId)> emails = await _imapEmailReader.GetUnreadAsync();
List<MailKit.UniqueId> processedMessageIds = new();
foreach (var (email, uid) in emails)
{
    try
    {
        var alert = _crimeEmailParser.Parse(email.TextBody);
        foreach (var crime in alert.Incidents)
        {
            _jsonCrimeWriter.Write(crime);
        }
        processedMessageIds.Add(uid);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex.Message);
        continue;
    }
}

await _imapEmailReader.MarkReadAsync(processedMessageIds);
```

Json files will be created for each of the incidents that was found in each of the emails.


## Contributions

To contribute, you may 

* create an issue using the appropriate template on the project repository.
* create a pull request with your code changes to the project repository.

### Project Repository

[https://github.com/almostengr/Almostengr.CrimeMappingCom.EmailParser](https://github.com/almostengr/Almostengr.CrimeMappingCom.EmailParser)


## License

GNU GENERAL PUBLIC LICENSE. See LICENSE for more details.


## Project Roadmap

Some additional features that may be considered or implemented in the future

* support POP email
