package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class EnvelopeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EnvelopeEntity>(EnvelopeTable)

    var secondaryId by EnvelopeTable.secondaryId
    var externalId by EnvelopeTable.externalId
    var type by EnvelopeTable.type
    var createdAt by EnvelopeTable.createdAt
    var updatedAt by EnvelopeTable.updatedAt
    var completedAt by EnvelopeTable.completedAt
    var deletedAt by EnvelopeTable.deletedAt
    var title by EnvelopeTable.title
    var status by EnvelopeTable.status
    var source by EnvelopeTable.source1
    var qrToken by EnvelopeTable.qrToken
    var signatureLevel by EnvelopeTable.signatureLevel
    var internalVersion by EnvelopeTable.internalVersion
    var useLegacyFieldInsertion by EnvelopeTable.useLegacyFieldInsertion
    val envelopeItems by EnvelopeItemEntity referrersOn EnvelopeItemTable.envelopeId
    val recipients by RecipientEntity referrersOn RecipientTable.envelopeId
    val fields by FieldEntity referrersOn FieldTable.envelopeId
    val shareLinks by DocumentShareLinkEntity referrersOn DocumentShareLinkTable.envelopeId
    val auditLogs by DocumentAuditLogEntity optionalReferrersOn DocumentAuditLogTable.envelopeId
    var authOptions by EnvelopeTable.authOptions
    var formValues by EnvelopeTable.formValues
    var visibility by EnvelopeTable.visibility
    var templateType by EnvelopeTable.templateType
    var publicTitle by EnvelopeTable.publicTitle
    var publicDescription by EnvelopeTable.publicDescription
    val directLink by TemplateDirectLinkEntity optionalBackReferencedOn TemplateDirectLinkTable.envelopeId
    var templateId by EnvelopeTable.templateId
    var userId by EnvelopeTable.userId
    var user by UserEntity referencedOn EnvelopeTable.userId
    var teamId by EnvelopeTable.teamId
    var team by TeamEntity referencedOn EnvelopeTable.teamId
    var folder by FolderEntity optionalReferencedOn EnvelopeTable.folderId
    var folderId by EnvelopeTable.folderId
    var documentMetaId by EnvelopeTable.documentMetaId
    var documentMeta by DocumentMetaEntity referencedOn EnvelopeTable.documentMetaId
    val envelopeAttachments by EnvelopeAttachmentEntity referrersOn EnvelopeAttachmentTable.envelopeId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EnvelopeTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
