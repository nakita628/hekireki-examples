package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ConfigurationObjectLinkEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ConfigurationObjectLinkEntity>(ConfigurationObjectLinkTable)

    var deleted by ConfigurationObjectLinkTable.deleted
    var createdAt by ConfigurationObjectLinkTable.createdAt
    var updatedAt by ConfigurationObjectLinkTable.updatedAt
    var type by ConfigurationObjectLinkTable.type
    var data by ConfigurationObjectLinkTable.data
    var workspaceId by ConfigurationObjectLinkTable.workspaceId
    var workspace by WorkspaceEntity referencedOn ConfigurationObjectLinkTable.workspaceId
    var fromId by ConfigurationObjectLinkTable.fromId
    var from by ConfigurationObjectEntity referencedOn ConfigurationObjectLinkTable.fromId
    var toId by ConfigurationObjectLinkTable.toId
    var to by ConfigurationObjectEntity referencedOn ConfigurationObjectLinkTable.toId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ConfigurationObjectLinkTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
