import React from 'react';
import { Container } from 'reactstrap';
import NavMenu from '../components/NavMenu';
import Footer from '../components/Footer';


function Layout({ children, show }) {

    return (

        <div>
            <NavMenu />

            <Container tag="main">
                {children}
            </Container>
            <Footer />

        </div>

        )

}
export default Layout

/*
 the props work as a container. that will display all the content from our components
 */
