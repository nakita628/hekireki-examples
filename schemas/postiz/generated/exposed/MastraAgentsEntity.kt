package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraAgentsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraAgentsEntity>(MastraAgentsTable)

    var status by MastraAgentsTable.status
    var activeVersionId by MastraAgentsTable.activeVersionId
    var authorId by MastraAgentsTable.authorId
    var metadata by MastraAgentsTable.metadata
    var createdAt by MastraAgentsTable.createdAt
    var updatedAt by MastraAgentsTable.updatedAt
    var createdAtZ by MastraAgentsTable.createdAtZ
    var updatedAtZ by MastraAgentsTable.updatedAtZ
    var visibility by MastraAgentsTable.visibility
    var favoriteCount by MastraAgentsTable.favoriteCount
}
