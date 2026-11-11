package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMManagedObjectTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMManagedObjectTableEntity>(LiteLLMManagedObjectTableTable)

    var unifiedObjectId by LiteLLMManagedObjectTableTable.unifiedObjectId
    var modelObjectId by LiteLLMManagedObjectTableTable.modelObjectId
    var fileObject by LiteLLMManagedObjectTableTable.fileObject
    var filePurpose by LiteLLMManagedObjectTableTable.filePurpose
    var status by LiteLLMManagedObjectTableTable.status
    var batchProcessed by LiteLLMManagedObjectTableTable.batchProcessed
    var createdAt by LiteLLMManagedObjectTableTable.createdAt
    var createdBy by LiteLLMManagedObjectTableTable.createdBy
    var teamId by LiteLLMManagedObjectTableTable.teamId
    var orgId by LiteLLMManagedObjectTableTable.orgId
    var apiKey by LiteLLMManagedObjectTableTable.apiKey
    var requestTags by LiteLLMManagedObjectTableTable.requestTags
    var endUserId by LiteLLMManagedObjectTableTable.endUserId
    var updatedAt by LiteLLMManagedObjectTableTable.updatedAt
    var updatedBy by LiteLLMManagedObjectTableTable.updatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMManagedObjectTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
