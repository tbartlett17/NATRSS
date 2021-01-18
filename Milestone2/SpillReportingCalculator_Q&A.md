# Spill reporting Calculator

## Q: Why is this an original idea? What are related websitres apps?

Working in industry, I found that there was gap in how facilities can assess the point in which a release is classified as reportable. The information is scattered through variuous sources from the government. I tried looking for calculators or other tools for aiding facilities in making this determination on if a release has exceeded the reportable quantity but my searches lead dead ends for previously developed tools that are no longer accessible, outdated, or only existed in pdfs and other documents. 

## Q: Why is this idea worth doing? Why is it useful and not boring?

It is useful because it will help facilities with reporting spills in accordance to the reporting requirements in EPCRA sections 311 and 312 which will aid in better spill/emergency response. It also will give a facility information about the potential severity of their spill. 

## Q: What are a few major features?

1. An EHS representative should be able to add a facility to their list of Company Facilites.
2. An EHS representative should be able to select chemicals from the list of regulated substances and add them to their facilities 
2. An EHS representative should be able to manage a list of users for their facilities that could generate a spill report.
3. A user (EHS Representative or one of their site specific users) at a facility should be able to start a new report to determine if a spill is considered reportable or when it will become reporatable.  
    a. the user can select a facility.  
    b. select a chemical available at the chosen facility.  
    c. enter information about the spill []
4. Weather API to to fetch either current or historical weather forecast data dependent on location of facility and the date/time of the release entered by the user (required for determining amount spilled).    
5. Provide the user with contact information for which agencies need to be notified in the event of a release and a script for how to report a release to the specific agencies. 
6. Ability for user to export the report to a document if desired.

## Q: What resources will be required for you to complete this project that are not already included in the class?

[Python and pandas](https://pandas.pydata.org/) or [C# and HtmlAgilityPack Nuget Package](https://www.nuget.org/packages/HtmlAgilityPack/1.11.29?_src=template) to scrape the html tables that have the lists of [EPCRA Extremely Hazardous Substances](https://www.ecfr.gov/cgi-bin/text-idx?SID=5bda0c1c4736b83aaf402bed85944e07&mc=true&node=pt40.30.355&rgn=div5#ap40.30.355_161.a) and [CERCLA Hazardous Substances](https://www.ecfr.gov/cgi-bin/text-idx?node=pt40.30.302&rgn=div5#se40.30.302_14) that are required for the Spill Reporting Calculator project. 

List of APIs:  
[PubChem Pug Rest API](https://pubchemdocs.ncbi.nlm.nih.gov/pug-rest). The url for the search will contain the CAS No. (unique chemical ID) to retrieve the chemical id (CID) in the PubChem system.
[PubChem Pug View API](https://pubchemdocs.ncbi.nlm.nih.gov/pug-view) using the retrieved CID from the Pug Rest API call to retrieve the other relevant chemical information (Density, Vapor Pressure, Water Solubility, Water Reactivity, Flammability, Corrosivity).  
Weather API.

## Q: What algorithmic content is there in this project?

Will need to implement the evaporation algorithm available in [EPA's Technical Guidace for Hazards Analysis](https://www.epa.gov/sites/production/files/2013-08/documents/technical_guidance_for_hazard_analysis.pdf) Appendix G for determining amount of chemical evaporated which is important for determine the rate of release and amount released.

## Topic difficulty rating - 6/10

seems like we need a well thought out database design, lots of api work, and ensuring proper funtionality of our service to ensure generatored release reports adhere to the federal requlations for reporting 

