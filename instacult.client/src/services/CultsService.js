import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class CultsService {
  async getCults() {
    const res = await api.get('/api/cults')
    logger.log('Getting cults', res.data)
    AppState.cults = res.data
  }

  async getCult(id) {
    const res = await api.get('api/cults/' + id)
    logger.log('Get Cult', res.data)
    AppState.activeCult = res.data
  }

  async getCultists(id) {
    const res = await api.get(`api/cults/${id}/members`)
    logger.log('Cult Members', res.data)
    AppState.cultists = res.data
  }

  async create(cult) {
    const res = await api.post('/api/cults', cult)
    logger.log('Created Cult', res.data)
    AppState.cults.push(res.data)
  }

  async update(cult) {
    const res = await api.put('/api/cults/' + cult.id, cult)
    logger.log('cult updated', res.data)
    AppState.activeCult = res.data
  }

  async delete(id) {
    const res = await api.delete('api/cults/' + id)
    logger.log('destroyed cult', res.data)
    AppState.cults = AppState.cults.filter(c => c.id != id)
  }
}

export const cultsService = new CultsService()
