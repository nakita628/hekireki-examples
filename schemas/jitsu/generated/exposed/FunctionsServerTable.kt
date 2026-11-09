package jitsu.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object FunctionsServerTable : CompositeIdTable("\"FunctionsServer\"") {
    val workspaceId = text("workspaceId").entityId()
    val `class` = text("class").entityId()
    val deploymentId = text("deploymentId")
    val connections = array("connections", TextColumnType()).nullable()
    val emptyConnections = array("emptyConnections", TextColumnType()).nullable()
    val profileBuilders = array("profileBuilders", TextColumnType()).nullable()
    val createdAt = pgTimestamptz("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updatedAt", 3).nullable()
    override val primaryKey = PrimaryKey(workspaceId, `class`, name = "\"FunctionsServer_pkey\"")
}
