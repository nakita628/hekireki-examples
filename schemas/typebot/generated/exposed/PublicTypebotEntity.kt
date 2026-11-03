package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class PublicTypebotEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PublicTypebotEntity>(PublicTypebotTable)

    var version by PublicTypebotTable.version
    var createdAt by PublicTypebotTable.createdAt
    var updatedAt by PublicTypebotTable.updatedAt
    var lastActivityAt by PublicTypebotTable.lastActivityAt
    var typebotId by PublicTypebotTable.typebotId
    var groups by PublicTypebotTable.groups
    var events by PublicTypebotTable.events
    var variables by PublicTypebotTable.variables
    var edges by PublicTypebotTable.edges
    var theme by PublicTypebotTable.theme
    var settings by PublicTypebotTable.settings
    var typebot by TypebotEntity referencedOn PublicTypebotTable.typebotId
    val mediaIdsCache by RuntimeMediaIdCacheEntity referrersOn RuntimeMediaIdCacheTable.publicTypebotId
}
