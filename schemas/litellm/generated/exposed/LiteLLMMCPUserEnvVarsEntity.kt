package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMMCPUserEnvVarsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMMCPUserEnvVarsEntity>(LiteLLMMCPUserEnvVarsTable)

    var userId by LiteLLMMCPUserEnvVarsTable.userId
    var serverId by LiteLLMMCPUserEnvVarsTable.serverId
    var valuesB64 by LiteLLMMCPUserEnvVarsTable.valuesB64
    var createdAt by LiteLLMMCPUserEnvVarsTable.createdAt
    var updatedAt by LiteLLMMCPUserEnvVarsTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMMCPUserEnvVarsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
