package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AgreementResponseTable : IdTable<String>("\"AgreementResponse\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val agreementId =
        reference(
            "agreementId",
            AgreementTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AgreementResponse_agreementId_fkey\"",
        )
    val viewId =
        optReference(
            "viewId",
            ViewTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AgreementResponse_viewId_fkey\"",
        ).uniqueIndex("\"AgreementResponse_viewId_key\"")
    val linkId = text("linkId").nullable()
    val signerEmail = text("signerEmail").nullable()
    val signerName = text("signerName").nullable()
    val signingStatus = text("signingStatus").default("PENDING")
    val signingExternalId = text("signingExternalId").nullable().uniqueIndex("\"AgreementResponse_signingExternalId_key\"")
    val signingEnvelopeId = text("signingEnvelopeId").nullable()
    val signingDocumentId = integer("signingDocumentId").nullable()
    val signedAt = pgTimestamp("signedAt", 3).nullable()
    val completedAt = pgTimestamp("completedAt", 3).nullable()
    val signedFileKey = text("signedFileKey").nullable()
    val signedFileName = text("signedFileName").nullable()
    val signedFileStorageType =
        pgEnum("signedFileStorageType", "\"DocumentStorageType\"", DocumentStorageType.entries, DocumentStorageType::dbName).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"AgreementResponse_pkey\"")

    init {
        index("\"AgreementResponse_agreementId_idx\"", false, agreementId)
        index("\"AgreementResponse_viewId_idx\"", false, viewId)
        index("\"AgreementResponse_linkId_idx\"", false, linkId)
        index("\"AgreementResponse_signerEmail_idx\"", false, signerEmail)
    }
}
