import axios from 'axios';

export default {

  login(user) {
    return axios.post('/login', user);
  },
  register(user) {
    return axios.post('/login/register', user);
  },
  getAllUsers() {
    return axios.get('/login');
  },
}
