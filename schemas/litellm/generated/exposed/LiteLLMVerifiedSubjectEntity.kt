package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMVerifiedSubjectEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMVerifiedSubjectEntity>(LiteLLMVerifiedSubjectTable)

    var issuer by LiteLLMVerifiedSubjectTable.issuer
    var tenantId by LiteLLMVerifiedSubjectTable.tenantId
    var oid by LiteLLMVerifiedSubjectTable.oid
    var kind by LiteLLMVerifiedSubjectTable.kind
    var userId by LiteLLMVerifiedSubjectTable.userId
    var user by LiteLLMUserTableEntity optionalReferencedOn LiteLLMVerifiedSubjectTable.userId
    var verifiedVia by LiteLLMVerifiedSubjectTable.verifiedVia
    var verifiedAt by LiteLLMVerifiedSubjectTable.verifiedAt
}
