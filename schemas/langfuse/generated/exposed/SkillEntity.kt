package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SkillEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SkillEntity>(SkillTable)

    var createdAt by SkillTable.createdAt
    var updatedAt by SkillTable.updatedAt
    var projectId by SkillTable.projectId
    var project by ProjectEntity referencedOn SkillTable.projectId
    var createdBy by SkillTable.createdBy
    var name by SkillTable.name
    var description by SkillTable.description
    var frontmatter by SkillTable.frontmatter
    var version by SkillTable.version
    var tags by SkillTable.tags
    var labels by SkillTable.labels
    var commitMessage by SkillTable.commitMessage
    val files by SkillFileEntity referrersOn SkillFileTable.skillId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SkillTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
