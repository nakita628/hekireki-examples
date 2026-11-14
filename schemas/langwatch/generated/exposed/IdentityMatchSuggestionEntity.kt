package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class IdentityMatchSuggestionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, IdentityMatchSuggestionEntity>(IdentityMatchSuggestionTable)

    var organizationId by IdentityMatchSuggestionTable.organizationId
    var discoveredPersonId by IdentityMatchSuggestionTable.discoveredPersonId
    var userId by IdentityMatchSuggestionTable.userId
    var score by IdentityMatchSuggestionTable.score
    var computedAt by IdentityMatchSuggestionTable.computedAt
}
