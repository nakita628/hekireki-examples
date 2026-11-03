package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AnnouncementEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AnnouncementEntity>(AnnouncementTable)

    var title by AnnouncementTable.title
    var description by AnnouncementTable.description
    var color by AnnouncementTable.color
    var createdAt by AnnouncementTable.createdAt
}
