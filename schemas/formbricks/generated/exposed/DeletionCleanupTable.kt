package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DeletionCleanupTable : IdTable<String>("\"DeletionCleanup\"") {
    override val id = text("id").clientDefault { CUID.randomCUID2().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val kind = pgEnum("kind", "\"DeletionCleanupKind\"", DeletionCleanupKind.entries, DeletionCleanupKind::dbName)
    val organizationId = text("organizationId")
    val workspaceId = text("workspaceId")
    val surveyId = text("surveyId")
    val tenantIds = array("tenantIds", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val responseIds = array("responseIds", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val fileKeys = array("fileKeys", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val attempts = integer("attempts").default(0)
    val nextAttemptAt =
        pgTimestamp("nextAttemptAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastError = text("lastError").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"DeletionCleanup_pkey\"")

    init {
        index("\"DeletionCleanup_nextAttemptAt_idx\"", false, nextAttemptAt)
        index("\"DeletionCleanup_organizationId_created_at_idx\"", false, organizationId, createdAt)
    }
}
