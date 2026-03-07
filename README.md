# CrimeMapping.com Email Parser

## Table of Contents

* [Purpose](#purpose)
* [Installation](#installation)
* [Usage](#usage)
* [Contributions](#contributions)
* [License](#license)

## Purpose

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

Use the example code below for reading emails. Each of the emails will be written
to a separate file.

```csharp
var reader = new ImapEmailReader();
var parser = new CrimeEmailParser();
var writer = new JsonCrimeWriter();
var settings = new CrimeMappingSettings();

var emails = await reader.GetUnreadAsync(
    settings.Hostname, settings.PortNumber, settings.Username, settings.Password);

foreach (var email in emails)
{
    var alert = parser.Parse(email.TextBody);

    foreach (var crime in alert.Incidents)
    {
        writer.Write(crime);
    }
}
```


## Contributions

To contribute, create a pull request with your code changes to the project repository.


## License

See LICENSE for more details.
