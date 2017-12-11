import axios from 'axios'

const base = 'http://localhost:1890/api'

export const RequestLogin = params => { return axios.post(`${base}/login`, params).then(res => res) }
