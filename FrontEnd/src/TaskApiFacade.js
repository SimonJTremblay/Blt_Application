import axios from 'axios';

export const HTTP = axios.create({
  baseURL:'https://localhost:44305/api/Task',
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
  getFromDeliverable(deliverableId){
    return this.execute('GET', `/${deliverableId}`)
  },
  create(data) {
    return this.execute('POST', '/', data);
  },
  update(id, data) {
    return this.execute('PUT', `/${id}`, data)
  },
  delete(id) {
    return this.execute('DELETE', `/${id}`)
  }

}