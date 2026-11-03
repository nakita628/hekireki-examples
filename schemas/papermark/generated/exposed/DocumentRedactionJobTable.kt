package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DocumentRedactionJobTable : IdTable<String>("\"DocumentRedactionJob\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val documentId =
        reference(
            "documentId",
            DocumentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentRedactionJob_documentId_fkey\"",
        )
    val documentVersionId =
        reference(
            "documentVersionId",
            DocumentVersionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentRedactionJob_documentVersionId_fkey\"",
        )
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentRedactionJob_teamId_fkey\"",
        )
    val createdById =
        reference(
            "createdById",
            UserTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentRedactionJob_createdById_fkey\"",
        )
    val status = text("status").default("PENDING")
    val customTerms = array("customTerms", TextColumnType()).nullable()
    val reasons = array("reasons", TextColumnType()).nullable()
    val resultVersionId = text("resultVersionId").nullable()
    val error = text("error").nullable()
    val detectRunId = text("detectRunId").nullable()
    val applyRunId = text("applyRunId").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DocumentRedactionJob_pkey\"")

    init {
        index("\"DocumentRedactionJob_documentId_idx\"", false, documentId)
        index("\"DocumentRedactionJob_documentVersionId_idx\"", false, documentVersionId)
        index("\"DocumentRedactionJob_teamId_idx\"", false, teamId)
        index("\"DocumentRedactionJob_teamId_status_idx\"", false, teamId, status)
    }
}
