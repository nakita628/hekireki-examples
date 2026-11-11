package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMManagedFileTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMManagedFileTableEntity>(LiteLLMManagedFileTableTable)

    var unifiedFileId by LiteLLMManagedFileTableTable.unifiedFileId
    var fileObject by LiteLLMManagedFileTableTable.fileObject
    var modelMappings by LiteLLMManagedFileTableTable.modelMappings
    var flatModelFileIds by LiteLLMManagedFileTableTable.flatModelFileIds
    var storageBackend by LiteLLMManagedFileTableTable.storageBackend
    var storageUrl by LiteLLMManagedFileTableTable.storageUrl
    var createdAt by LiteLLMManagedFileTableTable.createdAt
    var createdBy by LiteLLMManagedFileTableTable.createdBy
    var teamId by LiteLLMManagedFileTableTable.teamId
    var updatedAt by LiteLLMManagedFileTableTable.updatedAt
    var updatedBy by LiteLLMManagedFileTableTable.updatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMManagedFileTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
