package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class BannedIpEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BannedIpEntity>(BannedIpTable)

    var createdAt by BannedIpTable.createdAt
    var ip by BannedIpTable.ip
    var responsibleTypebot by TypebotEntity referencedOn BannedIpTable.responsibleTypebotId
    var responsibleTypebotId by BannedIpTable.responsibleTypebotId
    var user by UserEntity referencedOn BannedIpTable.userId
    var userId by BannedIpTable.userId
}
