package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DocumentRedactionJobEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DocumentRedactionJobEntity>(DocumentRedactionJobTable)

    var documentId by DocumentRedactionJobTable.documentId
    var document by DocumentEntity referencedOn DocumentRedactionJobTable.documentId
    var documentVersionId by DocumentRedactionJobTable.documentVersionId
    var documentVersion by DocumentVersionEntity referencedOn DocumentRedactionJobTable.documentVersionId
    var teamId by DocumentRedactionJobTable.teamId
    var team by TeamEntity referencedOn DocumentRedactionJobTable.teamId
    var createdById by DocumentRedactionJobTable.createdById
    var createdBy by UserEntity referencedOn DocumentRedactionJobTable.createdById
    var status by DocumentRedactionJobTable.status
    var customTerms by DocumentRedactionJobTable.customTerms
    var reasons by DocumentRedactionJobTable.reasons
    var resultVersionId by DocumentRedactionJobTable.resultVersionId
    var error by DocumentRedactionJobTable.error
    var detectRunId by DocumentRedactionJobTable.detectRunId
    var applyRunId by DocumentRedactionJobTable.applyRunId
    val redactions by DocumentRedactionEntity referrersOn DocumentRedactionTable.jobId
    var createdAt by DocumentRedactionJobTable.createdAt
    var updatedAt by DocumentRedactionJobTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DocumentRedactionJobTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
