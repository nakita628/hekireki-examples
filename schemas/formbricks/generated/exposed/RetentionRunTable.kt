package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RetentionRunTable : IdTable<String>("\"RetentionRun\"") {
    override val id = text("id").clientDefault { CUID.randomCUID2().toString() }.entityId()
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RetentionRun_organizationId_fkey\"",
        )
    val entity = pgEnum("entity", "\"RetentionEntity\"", RetentionEntity.entries, RetentionEntity::dbName)
    val kind = pgEnum("kind", "\"RetentionRunKind\"", RetentionRunKind.entries, RetentionRunKind::dbName).default(RetentionRunKind.SWEEP)
    val startedAt = pgTimestamp("startedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val finishedAt = pgTimestamp("finishedAt", 3).nullable()
    val notifiedCount = integer("notifiedCount").default(0)
    val archivedCount = integer("archivedCount").default(0)
    val deletedCount = integer("deletedCount").default(0)
    val skippedCount = integer("skippedCount").default(0)
    val hasChanges = bool("hasChanges").default(false)
    val scanCursor = text("scanCursor").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"RetentionRun_pkey\"")

    init {
        index("\"RetentionRun_organizationId_startedAt_id_idx\"", false, organizationId, startedAt, id)
    }
}
