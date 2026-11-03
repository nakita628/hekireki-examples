package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SkillBlobEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SkillBlobEntity>(SkillBlobTable)

    var createdAt by SkillBlobTable.createdAt
    var projectId by SkillBlobTable.projectId
    var project by ProjectEntity referencedOn SkillBlobTable.projectId
    var sha256Hash by SkillBlobTable.sha256Hash
    var contentType by SkillBlobTable.contentType
    var contentLength by SkillBlobTable.contentLength
    var content by SkillBlobTable.content
    val files by SkillFileEntity referrersOn SkillFileTable.blobId
}
