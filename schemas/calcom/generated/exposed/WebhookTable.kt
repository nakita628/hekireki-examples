package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object WebhookTable : IdTable<String>("\"Webhook\"") {
    override val id = text("id").entityId()
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Webhook_userId_fkey\"",
        )
    val teamId =
        optReference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Webhook_teamId_fkey\"",
        )
    val eventTypeId =
        optReference(
            "eventTypeId",
            EventTypeTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Webhook_eventTypeId_fkey\"",
        )
    val platformOAuthClientId =
        optReference(
            "platformOAuthClientId",
            PlatformOAuthClientTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Webhook_platformOAuthClientId_fkey\"",
        )
    val subscriberUrl = text("subscriberUrl")
    val payloadTemplate = text("payloadTemplate").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val active = bool("active").default(true)
    val eventTriggers =
        array("eventTriggers", PgEnumColumnType("\"WebhookTriggerEvents\"", WebhookTriggerEvents.entries, WebhookTriggerEvents::dbName))
            .nullable()
    val appId =
        optReference(
            "appId",
            AppTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Webhook_appId_fkey\"",
        )
    val secret = text("secret").nullable()
    val platform = bool("platform").default(false)
    val time = integer("time").nullable()
    val timeUnit = pgEnum("timeUnit", "\"TimeUnit\"", TimeUnit.entries, TimeUnit::dbName).nullable()
    val version = text("version").default("2021-10-20")
    override val primaryKey = PrimaryKey(id, name = "\"Webhook_pkey\"")

    init {
        uniqueIndex("\"Webhook_id_key\"", id)
        index("\"Webhook_active_idx\"", false, active)
        uniqueIndex("\"Webhook_userId_subscriberUrl_key\"", userId, subscriberUrl)
        uniqueIndex("\"Webhook_platformOAuthClientId_subscriberUrl_key\"", platformOAuthClientId, subscriberUrl)
    }
}
