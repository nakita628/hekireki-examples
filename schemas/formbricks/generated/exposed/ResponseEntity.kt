package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ResponseEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ResponseEntity>(ResponseTable)

    var createdAt by ResponseTable.createdAt
    var updatedAt by ResponseTable.updatedAt
    var finished by ResponseTable.finished
    var survey by SurveyEntity referencedOn ResponseTable.surveyId
    var surveyId by ResponseTable.surveyId
    var contact by ContactEntity optionalReferencedOn ResponseTable.contactId
    var contactId by ResponseTable.contactId
    var endingId by ResponseTable.endingId
    var data by ResponseTable.data
    var variables by ResponseTable.variables
    var ttc by ResponseTable.ttc
    var meta by ResponseTable.meta
    var ingestFlags by ResponseTable.ingestFlags
    var outcome by ResponseTable.outcome
    val tags by TagsOnResponsesEntity referrersOn TagsOnResponsesTable.responseId
    val quotaLinks by ResponseQuotaLinkEntity referrersOn ResponseQuotaLinkTable.responseId
    var contactAttributes by ResponseTable.contactAttributes
    var singleUseId by ResponseTable.singleUseId
    var language by ResponseTable.language
    var displayId by ResponseTable.displayId
    var display by DisplayEntity optionalReferencedOn ResponseTable.displayId
    val workflowRuns by WorkflowRunEntity optionalReferrersOn WorkflowRunTable.responseId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ResponseTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
