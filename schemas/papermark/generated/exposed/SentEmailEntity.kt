package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SentEmailEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SentEmailEntity>(SentEmailTable)

    var type by SentEmailTable.type
    var recipient by SentEmailTable.recipient
    var marketing by SentEmailTable.marketing
    var createdAt by SentEmailTable.createdAt
    var team by TeamEntity referencedOn SentEmailTable.teamId
    var teamId by SentEmailTable.teamId
    var domainSlug by SentEmailTable.domainSlug
}
