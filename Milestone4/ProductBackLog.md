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
            <td>1</td> <td>Completed</td> <td>U</td> <td>1</td> <td>Nick</td>
            <td>As a visitor to the site I would like to see a fantastic and modern homepage that introduces me to the site and the features currently available</td>
            <td>This is the initial setup story and will create a homepage for the coming project. There aren't any features yet so this will create a welcome page and will remove all boilerplate that isn't needed.</td>
            <td></td>
        </tr>
        <tr>
            <td>2</td> <td>Completed</td> <td>U</td> <td>2</td> <td>Tyler</td>
            <td>Being able to login to the site as a certain type of user</td>
            <td>This will add login and register pages. A user can register as a climber or expedition provider (aka trekking agency). An employee role will be created on the backend. Create an admin page for employees where they can access the db tables to perform CRUD operations. </td>
            <td></td>
        </tr>
        <tr>
            <td>3</td> <td>Completed</td> <td>U</td> <td>2</td> <td>Reggie</td>
            <td>As a public user I would like to be able to see quick statistics(dynamic information) about expeditions, mountains, and other facts so that I can stay informed.</td>
            <td>I will create a view that outputs facts on peak height, average height, number of peaks, number of climbs in the last 10 years</td>
            <td></td>
        </tr>
        <tr>
            <td>4</td> <td>Completed</td> <td>U</td> <td>1</td> <td>Armani</td>
            <td>As a user I would like to be able to see the top 50 expeditions by date, so that I can see what has been recently climbed</td>
            <td>I will create a page that will display a table of expedition information. I will then in the controller make it so that the top 50 expeditions are only shown and have that least be sorted by the most recent date.</td>
            <td></td>
        </tr>
        <tr>
            <td>5</td> <td>Completed</td> <td>U</td> <td>2</td> <td>Nick</td>
            <td>On the expeditions list page I would like to be able to sort the list by success, season, and by a specific peak</td>
            <td>On the view page I will create buttons to sort by peak, success, and the date. I will also create a drop down for each peak in the database. I will then in the home controller create functions for each sort and to display the drop down. Then when each button is selected it will sort or show the data that the user would like to see.</td>
            <td><a href="https://github.com/NickApa/NATRSS/blob/dev/Milestone4/UserStoryNick.md">Link to User Story</a></td>
        </tr>
        <tr>
            <td>6</td> <td>Completed</td> <td>E</td> <td>2</td> <td>Tyler</td>
            <td>As a user I would like to see my account information.</td>
            <td>I will make the login page redirect to an account view that shows the user profile. If the account type is an employee, there will be links to view all the tables and a link to the page to view support tickets for modifying expeditions submitted by the expedition provider (aka trekking agency). If the account type is an expedition provider (aka trekking agency)  there will also be a list of their entered expeditions.</td>
            <td><a href="https://github.com/NickApa/NATRSS/blob/dev/Milestone4/UserStory%236_Tyler.md">Link to User Story</a></td>
        </tr>
        <tr>
            <td>7</td> <td>Completed</td> <td>U</td> <td>2</td> <td>Reggie</td>
            <td>As an employee I would like to process request forms, review , see a list of all request forms and have full crud access to the database, to ensure validity of database entries, as well as update expeditions and review previous request forms.</td>
            <td>I will scaffold crud operation for users who are listed in the user table as an employee. I will create a view that lists all request forms for users that are employees.  Create a view for reviewing requests Expedition providers (aka trekking agency)  will only see their own request forms.</td>            
<td><a href="https://github.com/NickApa/NATRSS/blob/dev/Milestone4/UserStoryReggie.md">Link to User Story</a></td>
        </tr>
        <tr>
            <td>8</td> <td>Completed</td> <td>U</td> <td>2</td><td>Armani</td>
            <td>As a public user I would like to be able to browse or search for peaks so that I can easily find them.</td>
            <td>The stakeholder would like there to be a convential way to search for Peaks. They want to be able to browse for potential peaks within the database. In order for the peaks to be found in a convential manner the search will need to be inside the nav bar.</td>
            <td><a href="https://github.com/NickApa/NATRSS/blob/dev/Milestone4/UserStoryArmani.md">Link to User Story</a></td>
        </tr>
        <tr>
            <td>9</td> <td>Standby</td> <td>U</td> <td>2</td><td></td>
            <td>As a public user I would like to be able to refine my search results so that I can sort or retrieve data based on year or other attributes.</td>
            <td>The search bar will return data oriented details tied to the input of the search</td>
            <td></td>
        </tr>
        <tr>
            <td>10</td> <td>Standby</td> <td>U</td> <td>4</td><td></td>
            <td>As a user I would like to be able to share my experiences and tips on a social hub with fellow trekkers so that I can interact with other users on the site.</td>
            <td>Make a central feed page, a create post page. Posts will have title, contents, date posted, posted by user. Users need to be able to edit the post contents and be able to delete their posts.</td>
            <td></td>
        </tr>
        <tr>
            <td>11</td> <td>Standby</td> <td>U</td> <td>2</td><td></td>
            <td>As an employee I would like to monitor the user-provided content of the social hub so that I can, in terms of security, provide a safe online environment that complies with the social hub rules.</td>
            <td>Scaffold crud access to the post table in the database for users who are employees. </td>
            <td></td>
        </tr>
<tr>
            <td>12</td> <td>Standby</td> <td>U</td> <td>2</td><td></td>
            <td>As an employee I would like to monitor the user-provided content of the social hub so that I can, in terms of security, provide a safe online environment that complies with the social hub rules.</td>
            <td>Scaffold crud access to the post table in the database for users who are employees. </td>
            <td></td>
        </tr>
<tr>
            <td>13</td> <td>Standby</td> <td>U</td> <td>2</td> <td></td>
            <td>As an Expedition Provider (aka trekking agency) i'd like to be able to request a modification of one of my expeditions</td>
            <td>I will make it so that Expedition Providers (aka trekking agencies) can view their list of expeditions and have  the ability to request updates  to their entered expeditions. This will add a link to the table of expeditions on the account page to each of their expeditions which links to a form to request a modification of an expedition. This form once submitted will be added to the table of requests that is viewable by employees.</td>
            <td></td>
        </tr>
    </tbody>
</table>


