import axios from 'axios';

export const HTTP = axios.create({
  baseURL:'https://localhost:44305/api/project',
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
    return this.execute('GET',`/GetById/${id}`)
  },
  GetDeliverablesFromProject(id){
    return this.execute('GET',`/GetDeliverablesFromProject/${id}`)
  },
  create(data) {
    return this.execute('POST', '/', data)
  },
  update(id, data) {
    return this.execute('PUT', `/${id}`, data)
  },
  delete(id) {
    return this.execute('DELETE', `/${id}`)
  }

}