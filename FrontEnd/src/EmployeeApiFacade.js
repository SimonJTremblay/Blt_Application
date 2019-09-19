import axios from 'axios';

export const HTTP = axios.create({
  baseURL:'https://localhost:44305/api/employee',
  timeout: 2000,
  json:true
})

export default {
  async execute(method, resource, data) {
    return HTTP({
      method,
      url: resource,
      data
    }).then(req => {
      return req.data
    })
  },
  getAll() {
    return this.execute('GET', '/')
  },
  getById(id){
    return this.execute('GET',`/${id}`)
  }
}