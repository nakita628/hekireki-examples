package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object EnvelopeTable : IdTable<String>("\"Envelope\"") {
    override val id = text("id").entityId()
    val secondaryId = text("secondaryId").uniqueIndex("\"Envelope_secondaryId_key\"")
    val externalId = text("externalId").nullable()
    val type = pgEnum("type", "\"EnvelopeType\"", EnvelopeType.entries, EnvelopeType::dbName)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val completedAt = pgTimestamp("completedAt", 3).nullable()
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    val title = text("title")
    val status = pgEnum("status", "\"DocumentStatus\"", DocumentStatus.entries, DocumentStatus::dbName).default(DocumentStatus.DRAFT)
    val source1 = pgEnum("source", "\"DocumentSource\"", DocumentSource.entries, DocumentSource::dbName)
    val qrToken = text("qrToken").nullable()
    val signatureLevel = text("signatureLevel")
    val internalVersion = integer("internalVersion")
    val useLegacyFieldInsertion = bool("useLegacyFieldInsertion").default(false)
    val authOptions = jsonb<String>("authOptions", { it }, { it }).nullable()
    val formValues = jsonb<String>("formValues", { it }, { it }).nullable()
    val visibility =
        pgEnum("visibility", "\"DocumentVisibility\"", DocumentVisibility.entries, DocumentVisibility::dbName).default(
            DocumentVisibility.EVERYONE,
        )
    val templateType = pgEnum("templateType", "\"TemplateType\"", TemplateType.entries, TemplateType::dbName).default(TemplateType.PRIVATE)
    val publicTitle = text("publicTitle").default("")
    val publicDescription = text("publicDescription").default("")
    val templateId = integer("templateId").nullable()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Envelope_userId_fkey\"",
        )
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Envelope_teamId_fkey\"",
        )
    val folderId =
        optReference(
            "folderId",
            FolderTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Envelope_folderId_fkey\"",
        )
    val documentMetaId =
        reference(
            "documentMetaId",
            DocumentMetaTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Envelope_documentMetaId_fkey\"",
        ).uniqueIndex("\"Envelope_documentMetaId_key\"")
    override val primaryKey = PrimaryKey(id, name = "\"Envelope_pkey\"")

    init {
        index("\"Envelope_type_idx\"", false, type)
        index("\"Envelope_status_idx\"", false, status)
        index("\"Envelope_userId_idx\"", false, userId)
        index("\"Envelope_teamId_idx\"", false, teamId)
        index("\"Envelope_folderId_idx\"", false, folderId)
        index("\"Envelope_createdAt_idx\"", false, createdAt)
    }
}
