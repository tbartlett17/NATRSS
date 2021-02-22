# Product Backlog

<table>
    <thead>
        <tr>
            <th>ID</th> <th>State</th> <th>Story Type</th> <th>Points</th> <th>Owner</th>
            <th>Title</th>
            <th>Description</th>
            <th>Links</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>1</td> <td>Started</td> <td>U</td> <td>2</td> <td>Nick</td>
            <td>As a user I would like the website to have a database set up</td>
            <td>Create a database up script from the db diagram, create a seed script to add data to the database</td>
            <td></td>
        </tr>
        <tr>
            <td>2</td><td>Started</td> <td>U</td> <td>2</td> <td>Nick</td>
            <td>As a user I would like to have a azure site created</td>
            <td>Construct and build Website, scaffold the project based off of the up and seed scripts</td>
            <td></td>
        </tr>
        <tr>
            <td>3</td> <td>Started</td> <td>U</td> <td>2-4</td> <td>Armani</td>
            <td>As a user I would like a way to login to the website</td>
            <td>Set up Azure identity database so that users have a way to sign in with authorization</td>
            <td></td>
        </tr>
        <tr>
            <td>4</td> <td>Started</td> <td>U</td> <td>2</td> <td>Nick</td>
            <td>As a visitor to the site I would like to see a fantastic and modern homepage that introduces me to the site and the features currently available</td>
            <td>Create an index page that connects with a navbar to link other available pages, removes unnecessary boiler plate</td>
            <td></td>
        </tr>
        <tr>
            <td>5</td> <td>Started</td> <td>U</td> <td>4</td> <td>Tyler</td>
            <td>As a user I would like to be able to have chemicals scraped from the CFR</td>
            <td>install the HtmlAgilityPack Nouget Package, build a web scraper using HtmlAgilityPack to pull chemical cas no, chemical name, and reportable quantity off the html tables from the two lists of chemicals</td>
            <td></td>
        </tr>
        <tr>
            <td>6</td> <td>Started</td> <td>U</td> <td>2</td> <td>Tyler</td>
            <td>As a user I would like the scraped chemical data be added to the database</td>
            <td>Once the applicable data has been scrubbed with the HTMLAgilityPack it will be added to the database</td>
            <td></td>
        </tr>
        <tr>
            <td>7</td> <td>Started</td> <td>U</td> <td>4</td> <td>Reggie</td>
            <td>As a user I would like to have basic chemical data in the database and if it came from the EPCRA or CERCLA list</td>
            <td>for each chemical in the database make an api call to the PubChem Pug Rest api (using cas no) to retrieve PubChem's Compound ID (CID) and the molecular weight for that chemical, save the molecular weight to the chemical db.</td>
            <td></td>
        </tr>
        <tr>
            <td>8</td> <td>Started</td> <td>U</td> <td>2</td> <td>Reggie</td>
            <td>As a user I would like to have required chemical information in the database to perform equations, i.e. about their density, molecular weight, vapor pressure, water solubility, water reactivity, flamability, corrosivity, and if it came from the EPCRA or CERCLA list</td>
            <td>lookup the remaining information by making an api call to PubChem's Pug View api, parse the remaining data out into that chemicals properties and save the chemical to the database</td>
            <td></td>
        </tr>
        <tr>
            <td>9</td> <td>Started</td> <td>U</td> <td>2</td> <td>Tyler</td>
            <td>I would like for the azure site to run on a continuous update cycle so that the most recent version of the site is immediately accessible</td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>10</td> <td>Started</td> <td>U</td> <td>2</td> <td>Armani</td>
            <td>As a user I would like to be able to sign in as a facility manager</td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>11</td> <td>Started</td> <td>U</td> <td>2</td> <td>Armani</td>
            <td>As a user I would like to be able to sign in as an employee</td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>12</td> <td>-</td> <td></td> <td>-</td> <td></td>
            <td>As a facility manager I would like to be able to create a specific facility at my company</td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>13</td> <td>-</td> <td></td> <td>-</td> <td></td>
            <td>as a facility manager I would like add chemicals to my facilities</td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>14</td> <td>-</td> <td></td> <td>-</td> <td></td>
            <td>As a facility manager I'd like to add employees to my facilities.</td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>15</td> <td>-</td> <td></td> <td>-</td> <td></td>
            <td>As an employee I'd like to see my employee information.</td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>16</td> <td>-</td> <td>U</td> <td>-</td> <td></td>
            <td>As a User I'd like to fill out a spill report form</td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>17</td> <td>-</td> <td>U</td> <td>-</td> <td></td>
            <td>As a facility manager I'd like to see all spill reports for all my facilities.</td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>18</td> <td>-</td> <td>U</td> <td>-</td> <td></td>
            <td>As an employee I'd like to see all of my spill reports.</td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>19</td> <td>-</td> <td></td> <td>-</td> <td></td>
            <td>as a user when i enter spill information, i want my variable units to be converted to the units needed for the calculations / reporting</td>
            <td>create a set of unit conversion functions temperature</td>
            <td></td>
        </tr>
        <tr>
            <td>20</td> <td>-</td> <td></td> <td>-</td> <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>21</td> <td>-</td> <td></td> <td>-</td> <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>

    </tbody>
</table>