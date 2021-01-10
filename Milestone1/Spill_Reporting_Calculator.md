# Spill Reporting Calculator

## Background -

The US government has prepared a Consolidated List of Chemicals Subject to the Emergency Planning and Community Right-to-Know Act (EPCRA), Comprehensive Environmental Response, 
Compensation and Liability Act (CERCLA) and Section 112(r) of the Clean Air Act to help facilities handling chemicals determine whether they need to submit reports under sections 
302 and 313 of EPCRA and determine if releases of chemicals are reportable under section 102 and 103 of CERCLA and section 304 of EPCRA, as well as providing other information under 
other federal requirements.

[EPA's Consolidated List of Lists](https://www.epa.gov/sites/production/files/2015-03/documents/list_of_lists.pdf)  
EPCRA Section 304 Chemicals - [40 CFR Part 355 Appendix A Extremely Hazardous 
Substances](https://www.ecfr.gov/cgi-bin/text-idx?SID=5bda0c1c4736b83aaf402bed85944e07&mc=true&node=pt40.30.355&rgn=div5#ap40.30.355_161.a)  
CERCLA Section 102/103 Chemicals (require national response center notificaion) -[40CFR Part 302 Hazardous Substances]
(https://www.ecfr.gov/cgi-bin/text-idx?node=pt40.30.302&rgn=div5#se40.30.302_14)

## Problem Statement -

It is difficult to make a determination on if a spill exceeds the reportable quantity as there are many factors in play to making this determination, especially during an active spill. 
Failure to report a release can result in non-compliance with the federal codes and regulations.

## Solution -

Goal: To make an accurate determination if a spill has exceeded the reportable qunatity or is projected to exceed the reportable quantity at the current release rate. 
This determination should be able to be made in under 5 minutes and know which people/agencies to be called and what to tell them.

### API Usage

Weather API to to fetch either current or historical weather forecast data dependent on the time of the release entered by the user. Possible email api usage to send a release 
report to certain individuals at your facility. Possible api from the govt to fetch the relevant lists of chemicals and their reportable quantities (not sure if this exists though, 
other option is to scrape the website that displays this info).

### Database Usage

A facility EHS representative should be able to login to our website and manage their facility (note: a single EHS representative may have multiple facilities). This could look like the 
EHS representative being able to create a new facility, select chemicals from a list that they have at their facility, create logins/manage users for their facility. a user should be 
able to generate a report at their facility to determine if a release is reportable. reports should be saved to the database and accessible by users. Will need to save contact information 
for agenies that need to be contacted so this can be served to the user after determination of if a spill is reportable. Probably scrape the websites that display chemicals and their 
reportable quantities on a set frequency (These lists wont change frequently but we should ensure we have up-to-date information) to determine if any changes have occured and update the 
chemical list in the db. (this section will likely grow).

### Algorithmic Component

Compute user entered information about a spill and weather data (based on time of spill) to determine if the amount spilled exceeds the reportable quantity of the chemical 
that was spilled.
Equations to make this determination are available in [EPA's Technical Guidace for Hazards Analysis]
(https://www.epa.gov/sites/production/files/2013-08/documents/technical_guidance_for_hazard_analysis.pdf) Appendix G.
