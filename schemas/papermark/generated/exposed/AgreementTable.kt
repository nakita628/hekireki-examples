package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AgreementTable : IdTable<String>("\"Agreement\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val content = text("content")
    val contentType = text("contentType").default("LINK")
    val signingProvider = text("signingProvider").default("LEGACY")
    val signingExternalId = text("signingExternalId").nullable().uniqueIndex("\"Agreement_signingExternalId_key\"")
    val signingEnvelopeId = text("signingEnvelopeId").nullable()
    val signingTemplateId = text("signingTemplateId").nullable()
    val requireName = bool("requireName").default(true)
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Agreement_teamId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    val deletedBy = text("deletedBy").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Agreement_pkey\"")

    init {
        index("\"Agreement_teamId_idx\"", false, teamId)
    }
}
