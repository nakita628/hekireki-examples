package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class IdentityMatchEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, IdentityMatchEntity>(IdentityMatchTable)

    var organizationId by IdentityMatchTable.organizationId
    var discoveredPersonId by IdentityMatchTable.discoveredPersonId
    var userId by IdentityMatchTable.userId
    var evidenceKind by IdentityMatchTable.evidenceKind
    var validFrom by IdentityMatchTable.validFrom
    var validTo by IdentityMatchTable.validTo
    var createdAt by IdentityMatchTable.createdAt
}
