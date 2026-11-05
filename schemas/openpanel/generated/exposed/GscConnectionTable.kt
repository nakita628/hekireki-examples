package openpanel.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object GscConnectionTable : IdTable<UUID>("gsc_connections") {
    override val id = javaUUID("id").databaseDefault("gen_random_uuid()").entityId()
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"gsc_connections_projectId_fkey\"",
        ).uniqueIndex("\"gsc_connections_projectId_key\"")
    val siteUrl = text("siteUrl").default("")
    val accessToken = text("accessToken")
    val refreshToken = text("refreshToken")
    val accessTokenExpiresAt = pgTimestamp("accessTokenExpiresAt", 3).nullable()
    val lastSyncedAt = pgTimestamp("lastSyncedAt", 3).nullable()
    val lastSyncStatus = text("lastSyncStatus").nullable()
    val lastSyncError = text("lastSyncError").nullable()
    val backfillStatus = text("backfillStatus").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "gsc_connections_pkey")
}
