package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object BackgroundMigrationTable : IdTable<String>("background_migrations") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name").uniqueIndex("background_migrations_name_key")
    val script = text("script")
    val args = jsonb<String>("args", { it }, { it })
    val state = jsonb<String>("state", { it }, { it }).default("{}")
    val finishedAt = pgTimestamp("finished_at", 3).nullable()
    val failedAt = pgTimestamp("failed_at", 3).nullable()
    val failedReason = text("failed_reason").nullable()
    val workerId = text("worker_id").nullable()
    val lockedAt = pgTimestamp("locked_at", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "background_migrations_pkey")
}
