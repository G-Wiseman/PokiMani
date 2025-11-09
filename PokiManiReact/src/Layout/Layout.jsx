import Banner from "../Components/Banner/Banner";
import "./Layout.scss";

function Layout() {
    return (
        <>
            <div className="layout">
                <div className="layout__sidebar"></div>
                <div className="layout__banner">
                    <Banner />
                </div>
                <div className="layout__page"></div>
            </div>
        </>
    );
}

export default Layout;
