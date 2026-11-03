package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass
import java.util.UUID

class SessionEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, SessionEntity>(SessionTable)

    var websiteId by SessionTable.websiteId
    var browser by SessionTable.browser
    var os by SessionTable.os
    var device by SessionTable.device
    var screen by SessionTable.screen
    var language by SessionTable.language
    var country by SessionTable.country
    var region by SessionTable.region
    var city by SessionTable.city
    var distinctId by SessionTable.distinctId
    var createdAt by SessionTable.createdAt
}
