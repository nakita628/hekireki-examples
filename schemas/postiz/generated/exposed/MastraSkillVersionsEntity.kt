package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraSkillVersionsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraSkillVersionsEntity>(MastraSkillVersionsTable)

    var skillId by MastraSkillVersionsTable.skillId
    var versionNumber by MastraSkillVersionsTable.versionNumber
    var name by MastraSkillVersionsTable.name
    var description by MastraSkillVersionsTable.description
    var instructions by MastraSkillVersionsTable.instructions
    var license by MastraSkillVersionsTable.license
    var compatibility by MastraSkillVersionsTable.compatibility
    var source by MastraSkillVersionsTable.source1
    var references by MastraSkillVersionsTable.references
    var scripts by MastraSkillVersionsTable.scripts
    var assets by MastraSkillVersionsTable.assets
    var metadata by MastraSkillVersionsTable.metadata
    var tree by MastraSkillVersionsTable.tree
    var changedFields by MastraSkillVersionsTable.changedFields
    var changeMessage by MastraSkillVersionsTable.changeMessage
    var createdAt by MastraSkillVersionsTable.createdAt
    var createdAtZ by MastraSkillVersionsTable.createdAtZ
    var files by MastraSkillVersionsTable.files
}
