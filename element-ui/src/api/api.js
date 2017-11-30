import axios from 'axios'

const base = ''

export const RequestLogin = params => { return axios.post(`${base}/login`, params).then(res => res.data) }
