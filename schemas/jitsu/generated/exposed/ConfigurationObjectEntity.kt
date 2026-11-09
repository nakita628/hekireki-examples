package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ConfigurationObjectEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ConfigurationObjectEntity>(ConfigurationObjectTable)

    var deleted by ConfigurationObjectTable.deleted
    var createdAt by ConfigurationObjectTable.createdAt
    var updatedAt by ConfigurationObjectTable.updatedAt
    var type by ConfigurationObjectTable.type
    var workspaceId by ConfigurationObjectTable.workspaceId
    var workspace by WorkspaceEntity referencedOn ConfigurationObjectTable.workspaceId
    var config by ConfigurationObjectTable.config
    val fromLinks by ConfigurationObjectLinkEntity referrersOn ConfigurationObjectLinkTable.toId
    val toLinks by ConfigurationObjectLinkEntity referrersOn ConfigurationObjectLinkTable.fromId
    val profileBuilder by ProfileBuilderEntity optionalReferrersOn ProfileBuilderTable.destinationId
    val profileBuilderFunction by ProfileBuilderFunctionEntity referrersOn ProfileBuilderFunctionTable.functionId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ConfigurationObjectTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
