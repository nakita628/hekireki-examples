package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraPromptBlocksEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraPromptBlocksEntity>(MastraPromptBlocksTable)

    var status by MastraPromptBlocksTable.status
    var activeVersionId by MastraPromptBlocksTable.activeVersionId
    var authorId by MastraPromptBlocksTable.authorId
    var metadata by MastraPromptBlocksTable.metadata
    var createdAt by MastraPromptBlocksTable.createdAt
    var updatedAt by MastraPromptBlocksTable.updatedAt
    var createdAtZ by MastraPromptBlocksTable.createdAtZ
    var updatedAtZ by MastraPromptBlocksTable.updatedAtZ
}
