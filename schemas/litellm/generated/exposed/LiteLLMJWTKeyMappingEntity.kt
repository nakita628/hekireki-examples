package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMJWTKeyMappingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMJWTKeyMappingEntity>(LiteLLMJWTKeyMappingTable)

    var jwtIssuer by LiteLLMJWTKeyMappingTable.jwtIssuer
    var jwtClaimName by LiteLLMJWTKeyMappingTable.jwtClaimName
    var jwtClaimValue by LiteLLMJWTKeyMappingTable.jwtClaimValue
    var token by LiteLLMJWTKeyMappingTable.token
    var description by LiteLLMJWTKeyMappingTable.description
    var isActive by LiteLLMJWTKeyMappingTable.isActive
    var createdAt by LiteLLMJWTKeyMappingTable.createdAt
    var createdBy by LiteLLMJWTKeyMappingTable.createdBy
    var updatedAt by LiteLLMJWTKeyMappingTable.updatedAt
    var updatedBy by LiteLLMJWTKeyMappingTable.updatedBy
    var litellmVerificationToken by LiteLLMVerificationTokenEntity referencedOn LiteLLMJWTKeyMappingTable.token

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMJWTKeyMappingTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
