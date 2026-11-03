package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraChannelConfigEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraChannelConfigEntity>(MastraChannelConfigTable)

    var data by MastraChannelConfigTable.data
    var updatedAt by MastraChannelConfigTable.updatedAt
    var updatedAtZ by MastraChannelConfigTable.updatedAtZ
}
