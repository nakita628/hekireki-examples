package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object BulkActionGroupTable : IdTable<String>("\"BulkActionGroup\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val friendlyId = text("friendlyId").uniqueIndex("\"BulkActionGroup_friendlyId_key\"")
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BulkActionGroup_projectId_fkey\"",
        )
    val environmentId =
        optReference(
            "environmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BulkActionGroup_environmentId_fkey\"",
        )
    val type = pgEnum("type", "\"BulkActionType\"", BulkActionType.entries, BulkActionType::dbName)
    val status =
        pgEnum("status", "\"BulkActionStatus\"", BulkActionStatus.entries, BulkActionStatus::dbName).default(BulkActionStatus.PENDING)
    val queryName = text("queryName").nullable()
    val params = jsonb<String>("params", { it }, { it }).nullable()
    val dedupeKey = text("dedupeKey").nullable()
    val cursor = jsonb<String>("cursor", { it }, { it }).nullable()
    val successCount = integer("successCount").default(0)
    val failureCount = integer("failureCount").default(0)
    val totalCount = integer("totalCount").default(0)
    val completionNotification =
        pgEnum(
            "completionNotification",
            "\"BulkActionNotificationType\"",
            BulkActionNotificationType.entries,
            BulkActionNotificationType::dbName,
        ).default(BulkActionNotificationType.NONE)
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BulkActionGroup_userId_fkey\"",
        )
    val name = text("name").nullable()
    val completedAt = pgTimestamp("completedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"BulkActionGroup_pkey\"")

    init {
        index(
            "\"BulkActionGroup_environmentId_createdAt_idx\"",
            false,
            functions = listOf(indexExpression("\"environmentId\""), indexExpression("\"createdAt\" DESC")),
        )
        index("\"BulkActionGroup_environmentId_type_dedupeKey_idx\"", false, environmentId, type, dedupeKey)
        index("\"BulkActionGroup_environmentId_status_type_idx\"", false, environmentId, status, type)
    }
}
