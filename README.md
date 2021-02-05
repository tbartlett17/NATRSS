# Spill Tracker and Calculator

![spilltrackerReadmeLogo](Milestone5/spilltrackerReadmeLogo.png "NATRSS")

# Description
For environmental workers who need to report spills or look up information about chemicals if a report is needed. The Chemical Spill Tracker is an information system that will allow users to calculate whether or not a spill at the company or location will need to be reported to the proper authorities and if so, who to contact. 

We are creating this project as a continuation of our courses at Western Oregon University for the Computer Science Program. Courses in this program for the Senior Capstone are: CS460 - Software Engineering I, CS461 - Software Engineering II, CS462 - Software Engineering III. As well as prerequisite courses CS361 - Algorithms, CS364 - Information Systems. 

# Table of Contents
- Background
- Contributing
- Credits
- Access
- Team Rules
- Tools
- License

# Background
Under Title 40 Chapter I SubChapter J Parts 302 and 355 of the Code of Federal Regulations in the USA, requirements are established for facilites to submit emergency notification of chemical releases for designated chemicals on the [List of Hazardous Substances and Reportable Quantities](https://www.ecfr.gov/cgi-bin/text-idx?node=pt40.28.302&rgn=div5#se40.30.302_14) and [List of Extremely Hazardous Substances and Their Threshold Planning Quantities](https://www.ecfr.gov/cgi-bin/text-idx?SID=5bda0c1c4736b83aaf402bed85944e07&mc=true&node=pt40.30.355&rgn=div5#ap40.30.355_161.a).

We use equation 7 from the [EPA's Technical Guidance for Hazard Analysis Appx. G §2](https://www.epa.gov/sites/production/files/2013-08/documents/technical_guidance_for_hazard_analysis.pdf) to calculate the evaporation rate (rate of volatilization) for spilled pools of chemicals.

# Contributing
## Overall
- Follow MVC: Model, View, Controller
- Controllers and Models written in C#
- Views Written in HTML, Javascript, and CSS
- Add comments where code might be confusing or to help others understand its purpose
    
## C# Style
- Curly braces on their own line
- variable names in camel case, no '_' in between words  - "variableName"
- Class names must start with a capital and second word is also capital, no '_' in between words - "ClassName"
- Database context will be named db
    
## HTML Style
- Follow Bootstrap formatting 
- Follow color schema that is chosen in advance
- id names will be camel case - "variableName"

## CSS Style
- Format using in-line styling
- size things using % do not use px or ef
- Format after you get your functional code working

## JavaScript Style
- Function names in camel case - "functionName"
- Variable names in camel case - "variableName"
- Curly braces on their own line


## SQL Style
- Primary keys will be named 'ID'
- Foreign Keys will include the table and ID where it came from - "ChemicalID"
- Constraints will be sperated by a '_' - "Form_FK_Chemical"

## GIT
- Use separate feature branches.
- Only commit working code (Unless others require your code to help fix a bug) and commit often.
- Write meaningful commit messages.
- Pull the upstream development code and merge it into your feature branch before submitting a Pull Request.
- Pull Request must be made to the dev branch. All other pull requests will be rejected.
- GitHub Repo Master will merge the dev branch into the main branch after Sprint Meetings

# Credits 
- <a href="https://github.com/NickApa">Nick Apa</a>  

- <a href="https://github.com/atufagaWOU">Armani Tufaga</a>   

- <a href="https://github.com/tbartlett17">Tyler Bartlett</a> 

- <a href="https://github.com/edgyJackson">Reggie Johnson</a>  

# Access
Spill-Tracker.azurewebsites.net - Coming Soon!

# Team Rules
- Be Kind
- Work Efficiently

# Tools
- Microsoft Azure
- Identity Framework
- Core 3.1
- SQL Server
- C#, HTML, Javascript, CSS
- Continuous Deployment  

# License 
LGPL-3.0-or-later

For more info see <a href="https://github.com/NickApa/NATRSS/blob/dev/COPYING.LESSER.txt">License.md</a> file
