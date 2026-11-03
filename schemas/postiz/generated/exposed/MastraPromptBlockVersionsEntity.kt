package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraPromptBlockVersionsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraPromptBlockVersionsEntity>(MastraPromptBlockVersionsTable)

    var blockId by MastraPromptBlockVersionsTable.blockId
    var versionNumber by MastraPromptBlockVersionsTable.versionNumber
    var name by MastraPromptBlockVersionsTable.name
    var description by MastraPromptBlockVersionsTable.description
    var content by MastraPromptBlockVersionsTable.content
    var rules by MastraPromptBlockVersionsTable.rules
    var requestContextSchema by MastraPromptBlockVersionsTable.requestContextSchema
    var changedFields by MastraPromptBlockVersionsTable.changedFields
    var changeMessage by MastraPromptBlockVersionsTable.changeMessage
    var createdAt by MastraPromptBlockVersionsTable.createdAt
    var createdAtZ by MastraPromptBlockVersionsTable.createdAtZ
}
