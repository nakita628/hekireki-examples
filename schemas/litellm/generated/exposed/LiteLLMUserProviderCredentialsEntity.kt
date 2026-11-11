package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMUserProviderCredentialsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMUserProviderCredentialsEntity>(LiteLLMUserProviderCredentialsTable)

    var userId by LiteLLMUserProviderCredentialsTable.userId
    var credentialName by LiteLLMUserProviderCredentialsTable.credentialName
    var provider by LiteLLMUserProviderCredentialsTable.provider
    var credentialB64 by LiteLLMUserProviderCredentialsTable.credentialB64
    var createdAt by LiteLLMUserProviderCredentialsTable.createdAt
    var updatedAt by LiteLLMUserProviderCredentialsTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMUserProviderCredentialsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
