import axios from 'axios';

export const HTTP = axios.create({
  baseURL:'https://localhost:44305/api/Bluf',
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
  
    getById(id){
        return this.execute('GET',`/GetById/${id}`)
    },
    create(data) {
        return this.execute('POST', '/', data)
    },
}