package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ProjectActiveDayEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProjectActiveDayEntity>(ProjectActiveDayTable)

    var projectId by ProjectActiveDayTable.projectId
    var day by ProjectActiveDayTable.day
    var createdAt by ProjectActiveDayTable.createdAt
}
