package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMDeprecatedVerificationTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMDeprecatedVerificationTokenEntity>(LiteLLMDeprecatedVerificationTokenTable)

    var token by LiteLLMDeprecatedVerificationTokenTable.token
    var activeTokenId by LiteLLMDeprecatedVerificationTokenTable.activeTokenId
    var revokeAt by LiteLLMDeprecatedVerificationTokenTable.revokeAt
    var createdAt by LiteLLMDeprecatedVerificationTokenTable.createdAt
}
