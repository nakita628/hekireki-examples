package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LinkVisitorGroupEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LinkVisitorGroupEntity>(LinkVisitorGroupTable)

    var linkId by LinkVisitorGroupTable.linkId
    var link by LinkEntity referencedOn LinkVisitorGroupTable.linkId
    var visitorGroupId by LinkVisitorGroupTable.visitorGroupId
    var visitorGroup by VisitorGroupEntity referencedOn LinkVisitorGroupTable.visitorGroupId
    var createdAt by LinkVisitorGroupTable.createdAt
}
