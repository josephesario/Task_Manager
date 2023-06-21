import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faGithub, faLinkedin, faInstagram } from '@fortawesome/free-brands-svg-icons';


function Footer() {
    return (
        <footer className="footer">
            <div className="containerx container">
                <div className="social-links">
                    <a href="https://github.com/josephesario?tab=repositories">
                        <FontAwesomeIcon icon={faGithub} />
                    </a>
                    <a href="https://www.linkedin.com/in/jose-rodolfo-esapa-riochi-650112b6/">
                        <FontAwesomeIcon icon={faLinkedin} />
                    </a>
                    <a href="https://instagram.com/joseph_esario">
                        <FontAwesomeIcon icon={faInstagram} />
                    </a>
                </div>
                <div className="email">
                    <a href="JoserodolfoEsapa@gmail.com">MAIL</a>
                </div>
            </div>
        </footer>
    );
}

export default Footer;
