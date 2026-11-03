package typebot.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object WorkspaceTable : IdTable<String>("\"Workspace\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name")
    val icon = text("icon").nullable()
    val plan = pgEnum("plan", "\"Plan\"", Plan.entries, Plan::dbName).default(Plan.FREE)
    val stripeId = text("stripeId").nullable().uniqueIndex("\"Workspace_stripeId_key\"")
    val additionalChatsIndex = integer("additionalChatsIndex").default(0)
    val additionalStorageIndex = integer("additionalStorageIndex").default(0)
    val chatsLimitFirstEmailSentAt = pgTimestamp("chatsLimitFirstEmailSentAt", 3).nullable()
    val storageLimitFirstEmailSentAt = pgTimestamp("storageLimitFirstEmailSentAt", 3).nullable()
    val chatsLimitSecondEmailSentAt = pgTimestamp("chatsLimitSecondEmailSentAt", 3).nullable()
    val storageLimitSecondEmailSentAt = pgTimestamp("storageLimitSecondEmailSentAt", 3).nullable()
    val customChatsLimit = integer("customChatsLimit").nullable()
    val customStorageLimit = integer("customStorageLimit").nullable()
    val customSeatsLimit = integer("customSeatsLimit").nullable()
    val isQuarantined = bool("isQuarantined").default(false)
    val isSuspended = bool("isSuspended").default(false)
    val isPastDue = bool("isPastDue").default(false)
    val isVerified = bool("isVerified").nullable()
    val chatsHardLimit = integer("chatsHardLimit").nullable()
    val settings = jsonb<String>("settings", { it }, { it }).nullable()
    val lastActivityAt = pgTimestamp("lastActivityAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Workspace_pkey\"")
}
