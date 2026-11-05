package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ContactResearchEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ContactResearchEntity>(ContactResearchTable)

    var createdAt by ContactResearchTable.createdAt
    var email by ContactResearchTable.email
    var researchStartedAt by ContactResearchTable.researchStartedAt
    var found by ContactResearchTable.found
    var role by ContactResearchTable.role
    var confidence by ContactResearchTable.confidence
    var company by ContactResearchTable.company
    var sources by ContactResearchTable.sources
}
