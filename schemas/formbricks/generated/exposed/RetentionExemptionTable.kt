package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RetentionExemptionTable : IdTable<String>("\"RetentionExemption\"") {
    override val id = text("id").clientDefault { CUID.randomCUID2().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RetentionExemption_organizationId_fkey\"",
        )
    val entity = pgEnum("entity", "\"RetentionEntity\"", RetentionEntity.entries, RetentionEntity::dbName)
    val surveyId =
        optReference(
            "surveyId",
            SurveyTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RetentionExemption_surveyId_fkey\"",
        )
    val until = pgTimestamp("until", 3)
    val source1 =
        pgEnum("source", "\"RetentionExemptionSource\"", RetentionExemptionSource.entries, RetentionExemptionSource::dbName).default(
            RetentionExemptionSource.MANUAL,
        )
    val reason = text("reason").nullable()
    val createdById =
        optReference(
            "createdById",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RetentionExemption_createdById_fkey\"",
        )
    val revokedAt = pgTimestamp("revokedAt", 3).nullable()
    val revokedById =
        optReference(
            "revokedById",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RetentionExemption_revokedById_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"RetentionExemption_pkey\"")

    init {
        index("\"RetentionExemption_organizationId_created_at_id_idx\"", false, organizationId, createdAt, id)
        index("\"RetentionExemption_surveyId_idx\"", false, surveyId)
        index("\"RetentionExemption_createdById_idx\"", false, createdById)
        index("\"RetentionExemption_revokedById_idx\"", false, revokedById)
    }
}
