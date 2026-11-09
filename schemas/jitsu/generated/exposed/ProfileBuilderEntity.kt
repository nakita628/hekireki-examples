package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ProfileBuilderEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProfileBuilderEntity>(ProfileBuilderTable)

    var name by ProfileBuilderTable.name
    var deleted by ProfileBuilderTable.deleted
    var createdAt by ProfileBuilderTable.createdAt
    var updatedAt by ProfileBuilderTable.updatedAt
    var workspaceId by ProfileBuilderTable.workspaceId
    var workspace by WorkspaceEntity referencedOn ProfileBuilderTable.workspaceId
    var intermediateStorageCredentials by ProfileBuilderTable.intermediateStorageCredentials
    var destinationId by ProfileBuilderTable.destinationId
    var destination by ConfigurationObjectEntity optionalReferencedOn ProfileBuilderTable.destinationId
    var connectionOptions by ProfileBuilderTable.connectionOptions
    val functions by ProfileBuilderFunctionEntity referrersOn ProfileBuilderFunctionTable.profileBuilderId
    var version by ProfileBuilderTable.version

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ProfileBuilderTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
