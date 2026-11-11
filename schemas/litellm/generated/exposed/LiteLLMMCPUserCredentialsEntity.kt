package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMMCPUserCredentialsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMMCPUserCredentialsEntity>(LiteLLMMCPUserCredentialsTable)

    var userId by LiteLLMMCPUserCredentialsTable.userId
    var serverId by LiteLLMMCPUserCredentialsTable.serverId
    var credentialB64 by LiteLLMMCPUserCredentialsTable.credentialB64
    var createdAt by LiteLLMMCPUserCredentialsTable.createdAt
    var updatedAt by LiteLLMMCPUserCredentialsTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMMCPUserCredentialsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
