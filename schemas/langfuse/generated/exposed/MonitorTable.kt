package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MonitorTable : IdTable<String>("monitors") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy =
        optReference(
            "created_by",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "monitors_created_by_fkey",
        )
    val updatedBy =
        optReference(
            "updated_by",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "monitors_updated_by_fkey",
        )
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "monitors_project_id_fkey",
        )
    val view = pgEnum("view", "\"MonitorView\"", MonitorView.entries, MonitorView::dbName)
    val filters = jsonb<String>("filters", { it }, { it })
    val metric = jsonb<String>("metric", { it }, { it })
    val windowMs = long("window_ms")
    val cadenceMs = long("cadence_ms")
    val thresholdOperator =
        pgEnum("threshold_operator", "\"MonitorThresholdOperator\"", MonitorThresholdOperator.entries, MonitorThresholdOperator::dbName)
    val alertThreshold = decimal("alert_threshold", 65, 30)
    val warningThreshold = decimal("warning_threshold", 65, 30).nullable()
    val severity =
        pgEnum("severity", "\"MonitorSeverity\"", MonitorSeverity.entries, MonitorSeverity::dbName).default(MonitorSeverity.UNKNOWN)
    val severityChangedAt = pgTimestamp("severity_changed_at", 3).nullable()
    val noData = jsonb<String>("no_data", { it }, { it })
    val renotify = jsonb<String>("renotify", { it }, { it })
    val status = pgEnum("status", "\"MonitorStatus\"", MonitorStatus.entries, MonitorStatus::dbName).default(MonitorStatus.ACTIVE)
    val schedulerBatchId = long("scheduler_batch_id")
    val nextRunAt = pgTimestamp("next_run_at", 3).nullable()
    val lastPublishedAt = pgTimestamp("last_published_at", 3).nullable()
    val lastClaimedAt = pgTimestamp("last_claimed_at", 3).nullable()
    val lastCompletedAt = pgTimestamp("last_completed_at", 3).nullable()
    val name = text("name")
    val tags = array("tags", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val triggerIds = array("trigger_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val alertedAt = pgTimestamp("alerted_at", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "monitors_pkey")

    init {
        index("monitors_project_id_idx", false, projectId)
        index("monitors_scheduler_batch_id_idx", false, schedulerBatchId)
        index("monitors_scheduler_tick_idx", false, nextRunAt, schedulerBatchId)
    }
}
