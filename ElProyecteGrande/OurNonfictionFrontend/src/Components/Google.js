import { useEffect, useState } from 'react';
import { GoogleLogin } from 'react-google-login';
import { gapi } from 'gapi-script';
import { useNavigate } from 'react-router-dom';
import { postApi } from '../Clients/requests';

function Google({ clientId, setName }) {
  const [googleLogin, setGoogleLogin] = useState(false);
  const navigate = useNavigate();
  const id = clientId;

  useEffect(() => {
    const initClient = () => {
      gapi.auth2.init({
        clientId: id,
        scope: ''
      });
    };
    gapi.load('client:auth2', initClient);
  }, [id]);

  const OnSuccess = (res) => {
    if (!googleLogin) {
      setGoogleLogin(true);
      return;
    }

    setName(res.profileObj.name);
    const body = {
      "username":res.profileObj.name,
      "email":res.profileObj.email,
      "role":"User",
      "password":''
    };
    postApi('/account/signin-google', body).then((response)=>response.json()).then(data => {
      sessionStorage.setItem('token', data.token);
      sessionStorage.setItem('role',data.role);
    }).then(() => navigate('/calendar'))
  };

  const OnFailure = (err) => {
    console.log('failed:', err);
  };

  return (
    <GoogleLogin
      clientId={clientId}
      render={(renderProps) => (
        <button className="form-control btn btn-primary" onClick={renderProps.onClick} disabled={renderProps.disabled}>
          Sign in with Goole
        </button>
      )}
      buttonText="Sign in with Google"
      onSuccess={OnSuccess}
      onFailure={OnFailure}
      cookiePolicy={'single_host_origin'}
      isSignedIn={true}
    />
  )
}

export default Google
