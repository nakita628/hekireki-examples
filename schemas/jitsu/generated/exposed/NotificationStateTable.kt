package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object NotificationStateTable : CompositeIdTable("\"NotificationState\"") {
    val workspaceId = text("workspaceId")
    val actorId = text("actorId").entityId()
    val type = text("type").entityId()
    val tableName1 = text("tableName").entityId()
    val channelId = text("channelId").entityId()
    val flappingSince = pgTimestamptz("flappingSince", 3).nullable()
    val lastNotification = pgTimestamptz("lastNotification", 3)
    val statusChangeId = long("statusChangeId")
    val error = text("error").nullable()
    override val primaryKey = PrimaryKey(channelId, actorId, type, tableName1, name = "\"NotificationState_pkey\"")
}
