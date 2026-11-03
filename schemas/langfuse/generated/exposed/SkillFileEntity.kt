package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SkillFileEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SkillFileEntity>(SkillFileTable)

    var createdAt by SkillFileTable.createdAt
    var projectId by SkillFileTable.projectId
    var project by ProjectEntity referencedOn SkillFileTable.projectId
    var skillId by SkillFileTable.skillId
    var skill by SkillEntity referencedOn SkillFileTable.skillId
    var blobId by SkillFileTable.blobId
    var blob by SkillBlobEntity referencedOn SkillFileTable.blobId
    var path by SkillFileTable.path
}
