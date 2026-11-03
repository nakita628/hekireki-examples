package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraChannelInstallationsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraChannelInstallationsEntity>(MastraChannelInstallationsTable)

    var platform by MastraChannelInstallationsTable.platform
    var agentId by MastraChannelInstallationsTable.agentId
    var status by MastraChannelInstallationsTable.status
    var webhookId by MastraChannelInstallationsTable.webhookId
    var data by MastraChannelInstallationsTable.data
    var configHash by MastraChannelInstallationsTable.configHash
    var error by MastraChannelInstallationsTable.error
    var createdAt by MastraChannelInstallationsTable.createdAt
    var updatedAt by MastraChannelInstallationsTable.updatedAt
    var createdAtZ by MastraChannelInstallationsTable.createdAtZ
    var updatedAtZ by MastraChannelInstallationsTable.updatedAtZ
}
