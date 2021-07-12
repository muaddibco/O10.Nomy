# O10.Nomy

This is project for investigating opportunities of combining capabilties of O10 and Rapyd platforms.

## How to run

1. Clone this repo
2. Go to the root folder of the repo
3. Run the following command:

`docker compose -f docker-compose.prod.yml up -d`

4. Open Chrome browser and navigate to `http://localhost:5004`
5. Right click at any place of the opened page and select "Inspect" or click CTRL+SHIFT+I
6. In the opened DevTools panel choose switch to mobile view (CTRL+SHIFT+M) and refresh the page (F5)

## How to use
There is a predefined demo user with email 'demo@nomy.com' and password 'qqq'.

There are also several predefined experts for demonstration purposes with the following emails and password 'qqq' for everyone:
1. floria.pham@nomy.com
2. donita.almanda@nomy.com
3. alana.hartshorn@nomy.com
4. joseph.yzaguirre@nomy.com
5. monty.escareno@nomy.com
6. nadine.ralston@nomy.com
7. holley.wilsey@nomy.com
8. dudley.luiz@nomy.com
9. phylicia.atilano@nomy.com
10. eddy.mahlum@nomy.com
11. laci.higuera@nomy.com
12. yuriko.cecil@nomy.com
13. marc.nuno@nomy.com
14. madonna.weick@nomy.com
15. florencia.fagg@nomy.com

Sign-in into the app with demo user credentials.

Now open another page and, ponce again, go to `http://localtion:5001`, right click at any place and choose 'Inspect'. Refresh the page.

Pick up any predefined account of expert and enter his/her email and the password 'qqq'.

Now go back to the page of your account, click on "Anonymously find an expert for your needs". Then, in the appeared list, choose the expert that you logged in with on another page.

Once again switch to the page of the expert and confirm starting of a session.

Swith back to your page and observe how zero-knowledge invoices will appear on the page. 
