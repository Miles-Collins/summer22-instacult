import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { api } from "./AxiosService.js"



class CultMembersService {
  async joinCult(newCultist) {
    logger.log(newCultist)
    const res = await api.post('api/cultmembers', newCultist)
    logger.log('new cultist', res.data)
    AppState.cultists.push(res.data)
  }

  async excommunicate(id) {
    const res = await api.delete('api/cultmembers/' + id)
    logger.log('Delete Cultist', res.data)
    Pop.toast(res.data, 'warning')
    // NOTE this works but not for transition effects
    // AppState.cultists = AppState.cultists.filter(clt => clt.cultMemberId != id)
    let index = AppState.cultists.findIndex(clt => clt.cultMemberId == id)
    AppState.cultists.splice(index, 1)
  }
}

export const cultMembersService = new CultMembersService()
