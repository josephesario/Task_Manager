import { Outlet } from 'react-router-dom'
import Completed_task from "../Pages/Completed_task"
import To_do from "../Pages/To_do"
import NavMenu from "../components/NavMenu"
import Footer from "../components/Footer"



//css
import '../style/style.css'
import '../style/footer.css';


function Home() {
    return (
        <>
            <NavMenu />
            <div>
                <Outlet />
            </div>
            <Footer />
        </>

    );
}

export default Home;