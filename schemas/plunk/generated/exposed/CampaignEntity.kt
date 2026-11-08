package plunk.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CampaignEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CampaignEntity>(CampaignTable)

    var name by CampaignTable.name
    var description by CampaignTable.description
    var status by CampaignTable.status
    var subject by CampaignTable.subject
    var body by CampaignTable.body
    var from by CampaignTable.from
    var fromName by CampaignTable.fromName
    var replyTo by CampaignTable.replyTo
    var type by CampaignTable.type
    var audienceType by CampaignTable.audienceType
    var audienceCondition by CampaignTable.audienceCondition
    var segment by SegmentEntity optionalReferencedOn CampaignTable.segmentId
    var segmentId by CampaignTable.segmentId
    var scheduledFor by CampaignTable.scheduledFor
    var totalRecipients by CampaignTable.totalRecipients
    var sentCount by CampaignTable.sentCount
    var deliveredCount by CampaignTable.deliveredCount
    var openedCount by CampaignTable.openedCount
    var clickedCount by CampaignTable.clickedCount
    var bouncedCount by CampaignTable.bouncedCount
    var complainedCount by CampaignTable.complainedCount
    var unsubscribedCount by CampaignTable.unsubscribedCount
    var archived by CampaignTable.archived
    var project by ProjectEntity referencedOn CampaignTable.projectId
    var projectId by CampaignTable.projectId
    val emails by EmailEntity optionalReferrersOn EmailTable.campaignId
    var sentAt by CampaignTable.sentAt
    var createdAt by CampaignTable.createdAt
    var updatedAt by CampaignTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CampaignTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
