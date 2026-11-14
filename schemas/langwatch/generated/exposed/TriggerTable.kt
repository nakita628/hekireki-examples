package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object TriggerTable : IdTable<String>("\"Trigger\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val name = text("name")
    val projectId = text("projectId")
    val action = pgEnum("action", "\"TriggerAction\"", TriggerAction.entries, TriggerAction::dbName)
    val triggerKind = pgEnum("triggerKind", "\"TriggerKind\"", TriggerKind.entries, TriggerKind::dbName).default(TriggerKind.AUTOMATION)
    val actionParams = jsonb<String>("actionParams", { it }, { it })
    val filters = jsonb<String>("filters", { it }, { it })
    val filterQuery = text("filterQuery").nullable()
    val lastRunAt = double("lastRunAt").default(0.0)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val active = bool("active").default(true)
    val pausedReason = text("pausedReason").nullable()
    val pausedAt = pgTimestamptz("pausedAt", 3).nullable()
    val message = text("message").nullable()
    val deleted = bool("deleted").default(false)
    val alertType = pgEnum("alertType", "\"AlertType\"", AlertType.entries, AlertType::dbName).nullable()
    val slackTemplateType = text("slackTemplateType").nullable()
    val slackTemplate = text("slackTemplate").nullable()
    val emailSubjectTemplate = text("emailSubjectTemplate").nullable()
    val emailBodyTemplate = text("emailBodyTemplate").nullable()
    val notificationCadence = text("notificationCadence").default("immediate")
    val traceDebounceMs = integer("traceDebounceMs").default(30000)
    val customGraphId = text("customGraphId").nullable().uniqueIndex("\"Trigger_customGraphId_key\"")
    override val primaryKey = PrimaryKey(id, name = "\"Trigger_pkey\"")

    init {
        index("\"Trigger_projectId_idx\"", false, projectId)
    }
}
