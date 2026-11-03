package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LanguageEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LanguageEntity>(LanguageTable)

    var createdAt by LanguageTable.createdAt
    var updatedAt by LanguageTable.updatedAt
    var code by LanguageTable.code
    var alias by LanguageTable.alias
    var workspace by WorkspaceEntity referencedOn LanguageTable.workspaceId
    var workspaceId by LanguageTable.workspaceId
    val surveyLanguages by SurveyLanguageEntity referrersOn SurveyLanguageTable.languageId
    val creativeLanguages by CreativeLanguageEntity referrersOn CreativeLanguageTable.languageId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LanguageTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
