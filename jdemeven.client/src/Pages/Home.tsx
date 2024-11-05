import FirstPage from "../Components/FirstPage";
import LogoutLink from "../Components/LogoutLink";
import AuthorizeView, { AuthorizedUser } from "../Components/AuthorizeView";
function Home() {
    return (
        <AuthorizeView>
            <span><LogoutLink>Logout<AuthorizedUser value="emal" /></LogoutLink></span>
             <FirstPage />
        </AuthorizeView>
    );
}

export default Home;