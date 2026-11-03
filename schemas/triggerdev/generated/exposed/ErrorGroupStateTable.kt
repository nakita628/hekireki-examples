package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ErrorGroupStateTable : IdTable<String>("\"ErrorGroupState\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ErrorGroupState_organizationId_fkey\"",
        )
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ErrorGroupState_projectId_fkey\"",
        )
    val environmentId =
        optReference(
            "environmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ErrorGroupState_environmentId_fkey\"",
        )
    val taskIdentifier = text("taskIdentifier")
    val errorFingerprint = text("errorFingerprint")
    val status =
        pgEnum("status", "\"ErrorGroupStatus\"", ErrorGroupStatus.entries, ErrorGroupStatus::dbName).default(ErrorGroupStatus.UNRESOLVED)
    val ignoredUntil = pgTimestamp("ignoredUntil", 3).nullable()
    val ignoredUntilOccurrenceRate = integer("ignoredUntilOccurrenceRate").nullable()
    val ignoredUntilTotalOccurrences = integer("ignoredUntilTotalOccurrences").nullable()
    val ignoredAtOccurrenceCount = long("ignoredAtOccurrenceCount").nullable()
    val ignoredAt = pgTimestamp("ignoredAt", 3).nullable()
    val ignoredReason = text("ignoredReason").nullable()
    val ignoredByUserId = text("ignoredByUserId").nullable()
    val resolvedAt = pgTimestamp("resolvedAt", 3).nullable()
    val resolvedInVersion = text("resolvedInVersion").nullable()
    val resolvedBy = text("resolvedBy").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ErrorGroupState_pkey\"")

    init {
        index("\"ErrorGroupState_environmentId_status_idx\"", false, environmentId, status)
        uniqueIndex("ErrorGroupState_environmentId_taskIdentifier_errorFingerpri_key", environmentId, taskIdentifier, errorFingerprint)
    }
}
