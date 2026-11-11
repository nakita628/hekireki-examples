package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMManagedVectorStoreIndexTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMManagedVectorStoreIndexTableEntity>(LiteLLMManagedVectorStoreIndexTableTable)

    var indexName by LiteLLMManagedVectorStoreIndexTableTable.indexName
    var litellmParams by LiteLLMManagedVectorStoreIndexTableTable.litellmParams
    var indexInfo by LiteLLMManagedVectorStoreIndexTableTable.indexInfo
    var createdAt by LiteLLMManagedVectorStoreIndexTableTable.createdAt
    var createdBy by LiteLLMManagedVectorStoreIndexTableTable.createdBy
    var updatedAt by LiteLLMManagedVectorStoreIndexTableTable.updatedAt
    var updatedBy by LiteLLMManagedVectorStoreIndexTableTable.updatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMManagedVectorStoreIndexTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
