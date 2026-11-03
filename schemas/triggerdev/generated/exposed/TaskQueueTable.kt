package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object TaskQueueTable : IdTable<String>("\"TaskQueue\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val friendlyId = text("friendlyId").uniqueIndex("\"TaskQueue_friendlyId_key\"")
    val name = text("name")
    val type = pgEnum("type", "\"TaskQueueType\"", TaskQueueType.entries, TaskQueueType::dbName).default(TaskQueueType.VIRTUAL)
    val version = pgEnum("version", "\"TaskQueueVersion\"", TaskQueueVersion.entries, TaskQueueVersion::dbName).default(TaskQueueVersion.V1)
    val orderableName = text("orderableName").nullable()
    val concurrencyVersion =
        pgEnum(
            "concurrencyVersion",
            "\"TaskQueueConcurrencyVersion\"",
            TaskQueueConcurrencyVersion.entries,
            TaskQueueConcurrencyVersion::dbName,
        ).default(TaskQueueConcurrencyVersion.V1)
    val role = pgEnum("role", "\"TaskQueueRole\"", TaskQueueRole.entries, TaskQueueRole::dbName).default(TaskQueueRole.QUEUE)
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskQueue_projectId_fkey\"",
        )
    val runtimeEnvironmentId =
        reference(
            "runtimeEnvironmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskQueue_runtimeEnvironmentId_fkey\"",
        )
    val concurrencyLimit = integer("concurrencyLimit").nullable()
    val concurrencyLimitOverriddenAt = pgTimestamp("concurrencyLimitOverriddenAt", 3).nullable()
    val concurrencyLimitOverriddenBy = text("concurrencyLimitOverriddenBy").nullable()
    val concurrencyLimitBase = integer("concurrencyLimitBase").nullable()
    val concurrencyLimitOverridePercent = decimal("concurrencyLimitOverridePercent", 5, 2).nullable()
    val totalConcurrencyLimit = integer("totalConcurrencyLimit").nullable()
    val totalConcurrencyLimitOverriddenAt = pgTimestamp("totalConcurrencyLimitOverriddenAt", 3).nullable()
    val totalConcurrencyLimitOverriddenBy = text("totalConcurrencyLimitOverriddenBy").nullable()
    val totalConcurrencyLimitBase = integer("totalConcurrencyLimitBase").nullable()
    val rateLimit = jsonb<String>("rateLimit", { it }, { it }).nullable()
    val paused = bool("paused").default(false)
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"TaskQueue_pkey\"")

    init {
        uniqueIndex("\"TaskQueue_runtimeEnvironmentId_name_key\"", runtimeEnvironmentId, name)
    }
}
