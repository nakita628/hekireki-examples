package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMCredentialsTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMCredentialsTableEntity>(LiteLLMCredentialsTableTable)

    var credentialName by LiteLLMCredentialsTableTable.credentialName
    var displayName by LiteLLMCredentialsTableTable.displayName
    var credentialValues by LiteLLMCredentialsTableTable.credentialValues
    var credentialInfo by LiteLLMCredentialsTableTable.credentialInfo
    var createdAt by LiteLLMCredentialsTableTable.createdAt
    var createdBy by LiteLLMCredentialsTableTable.createdBy
    var updatedAt by LiteLLMCredentialsTableTable.updatedAt
    var updatedBy by LiteLLMCredentialsTableTable.updatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMCredentialsTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
