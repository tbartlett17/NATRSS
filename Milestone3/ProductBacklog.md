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
            <td>1</td> <td>Completed</td> <td>U</td> <td>-</td> <td>Nick</td>
            <td>As a visitor to the site I would like to see a fantastic and modern homepage that introduces me to the site and the features currently available</td>
            <td>This is the initial setup story and will create a homepage for the coming project. There aren't any features yet so this will create a welcome page and will remove all boilerplate that isn't needed.</td>
            <td></td>
        </tr>
        <tr>
            <td>2</td> <td>Completed</td> <td>U</td> <td>-</td> <td>Tyler</td>
            <td>Being able to login to the site as a certain type of user</td>
            <td>This will add login and register pages. A user can register as a climber or expedition provider. An employee role will be created on the backend. </td>
            <td></td>
        </tr>
        <tr>
            <td>3</td> <td>Completed</td> <td>U</td> <td>-</td> <td>Reggie</td>
            <td>As a public user I would like to be able to see quick statistics(dynamic information) about expeditions, mountains, and other facts so that I can stay informed.</td>
            <td>I will create a view that outputs facts on peak height, average height, number of peaks, number of climbs in the last 10 years</td>
            <td></td>
        </tr>
        <tr>
            <td>4</td> <td>Completed</td> <td>U</td> <td>-</td> <td>Armani</td>
            <td>As a user I would like to be able to see the top 50 expeditions by date, I would also like to sort those expeditions by peak,success and date< as well as select a certain peak</td>
            <td>On the view page I will create buttons to sort by peak, success, and the date. I will also create a drop down for each peak in the database. I will then in the home controller create functions for each sort and to display the drop down. Then I will connect the button with the controller in order for when the button is pushed it actually does something. </td>
            <td></td>
        </tr>
        <tr>
            <td>5</td> <td>Standby</td> <td>U</td> <td>-</td> <td></td>
            <td>As a public user I would like to be able to browse or search for expeditions, climbers, dates, mountains, heights, etc. so that I can easily find them.</td>
            <td>This search bar will be used to return basic information related to expeditions, climbers, dates, mountains, heights, and any related information within the database.</td>
            <td></td>
        </tr>
        <tr>
            <td>6</td> <td>Standby</td> <td>E</td> <td>-</td> <td></td>
            <td>Expedition Provider Operations</td>
            <td>I will make it so that Expedition Providers can view their list of expeditions and have  the ability to request updates  to their entered expeditions. This will add a link to the table to each of their expeditions to request a modification. I'll make a modification page to submit a  request to modify an entry.</td>
            <td></td>
        </tr>
        <tr>
            <td>7</td> <td>Standby</td> <td>U</td> <td>-</td> <td></td>
            <td>As an employee I would like to process request forms, see a list of all request forms and have full crud access to the database, to ensure validity of database entries,as well as update expeditions and review previous request forms.</td>
            <td>I will scaffold crud operation for users who are listed in the user table as an employee. I will create a view that lists all request forms for users that are employees. Expedition providers will only see their own request forms. </td>
            <td></td>
        </tr>
        <tr>
            <td>8</td> <td>Standby</td> <td>U</td> <td>-</td> <td></td>
            <td>As a public user I would like to be able to refine my search results so that I can sort or retrieve data based on year or other attributes.</td>
            <td>The search bar will return data oriented details tied to the input of the search</td>
            <td></td>
        </tr>
        <tr>
            <td>9</td> <td>Standby</td> <td>U</td> <td>-</td> <td></td>
            <td>As an employee I would like to have to have access to an admin page where i can view support tickets
            for expedition updates that were submitted by expedition providers and be able to search for a record
            in the database to modify it.</td>
            <td>Create an admin page for employees where they can access the db tables to perform CRUD operations.</td>
            <td></td>
        <tr>
            <td>10</td> <td>Standby</td> <td>U</td> <td>-</td> <td></td>
            <td>As a user I would like to be able to share my experiences and tips on a social hub with fellow trekkers so that I can interact with other users on the site.</td>
            <td>Make a central feed page, a create post page. Posts will have title, contents, date posted, posted by user. Users need to be able to edit the post contents and be able to delete their posts.</td>
            <td></td>
        </tr>
        <tr>
            <td>11</td> <td>Standby</td> <td>U</td> <td>-</td> <td></td>
            <td>As an employee I would like to monitor the user-provided content of the social hub so that I can, in terms of security, provide a safe online environment that complies with the social hub rules.</td>
            <td>Scaffold crud access to the post table in the database for users who are employees. </td>
            <td></td>
        </tr>
    </tbody>
</table>


