package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MessagingRouteTable : IdTable<String>("\"MessagingRoute\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val purpose = pgEnum("purpose", "\"MessagingRoutePurpose\"", MessagingRoutePurpose.entries, MessagingRoutePurpose::dbName)
    val targetType =
        pgEnum("targetType", "\"MessagingRouteTargetType\"", MessagingRouteTargetType.entries, MessagingRouteTargetType::dbName)
    val targetId = text("targetId")
    val messagingChannelId =
        reference(
            "messagingChannelId",
            MessagingChannelTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MessagingRoute_messagingChannelId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"MessagingRoute_pkey\"")

    init {
        uniqueIndex("\"MessagingRoute_messagingChannelId_purpose_key\"", messagingChannelId, purpose)
    }
}
