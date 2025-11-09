import { Navigate } from 'react-router-dom';
import { usePokiManiApi } from '../../api/PokiManiAuthProvider';


export default function Redirect() {
  const { isAuthenticated } = usePokiManiApi();

  return (
    <Navigate to={isAuthenticated ? '/home' : '/login'} replace />
  );
}