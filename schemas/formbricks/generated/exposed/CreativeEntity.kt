package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CreativeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CreativeEntity>(CreativeTable)

    var createdAt by CreativeTable.createdAt
    var updatedAt by CreativeTable.updatedAt
    var name by CreativeTable.name
    var type by CreativeTable.type
    var status by CreativeTable.status
    var publishedAt by CreativeTable.publishedAt
    var archivedAt by CreativeTable.archivedAt
    var workspace by WorkspaceEntity referencedOn CreativeTable.workspaceId
    var workspaceId by CreativeTable.workspaceId
    var creator by UserEntity optionalReferencedOn CreativeTable.createdBy
    var createdBy by CreativeTable.createdBy
    var content by CreativeTable.content
    var styling by CreativeTable.styling
    var surveyId by CreativeTable.surveyId
    var linkUrl by CreativeTable.linkUrl
    var showOn by CreativeTable.showOn
    var showConditions by CreativeTable.showConditions
    var displayPercentage by CreativeTable.displayPercentage
    var startAt by CreativeTable.startAt
    var endAt by CreativeTable.endAt
    var autoSelectLanguage by CreativeTable.autoSelectLanguage
    val triggers by CreativeTriggerEntity referrersOn CreativeTriggerTable.creativeId
    val languages by CreativeLanguageEntity referrersOn CreativeLanguageTable.creativeId
    val dailyStats by CreativeDailyStatEntity referrersOn CreativeDailyStatTable.creativeId
    val dailyPageStats by CreativeDailyPageStatEntity referrersOn CreativeDailyPageStatTable.creativeId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CreativeTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
