package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMManagedVectorStoresTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMManagedVectorStoresTableEntity>(LiteLLMManagedVectorStoresTableTable)

    var customLlmProvider by LiteLLMManagedVectorStoresTableTable.customLlmProvider
    var vectorStoreName by LiteLLMManagedVectorStoresTableTable.vectorStoreName
    var vectorStoreDescription by LiteLLMManagedVectorStoresTableTable.vectorStoreDescription
    var vectorStoreMetadata by LiteLLMManagedVectorStoresTableTable.vectorStoreMetadata
    var createdAt by LiteLLMManagedVectorStoresTableTable.createdAt
    var updatedAt by LiteLLMManagedVectorStoresTableTable.updatedAt
    var litellmCredentialName by LiteLLMManagedVectorStoresTableTable.litellmCredentialName
    var litellmParams by LiteLLMManagedVectorStoresTableTable.litellmParams
    var teamId by LiteLLMManagedVectorStoresTableTable.teamId
    var userId by LiteLLMManagedVectorStoresTableTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMManagedVectorStoresTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
