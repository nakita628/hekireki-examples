package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object ProfileBuilderStateTable : CompositeIdTable("\"ProfileBuilderState\"") {
    val profileBuilderId = text("profileBuilderId").entityId()
    val profileBuilderVersion = integer("profileBuilderVersion").entityId()
    val instanceIndex = integer("instanceIndex").entityId()
    val totalInstances = integer("totalInstances").entityId()
    val startedAt = pgTimestamp("startedAt", 3)
    val updatedAt = pgTimestamp("updatedAt", 3)
    val lastTimestamp = pgTimestamp("lastTimestamp", 3).nullable()
    val processedUsers = integer("processedUsers")
    val totalUsers = integer("totalUsers")
    val errorUsers = integer("errorUsers")
    val speed = double("speed")
    override val primaryKey =
        PrimaryKey(profileBuilderId, profileBuilderVersion, totalInstances, instanceIndex, name = "\"ProfileBuilderState_pkey\"")
}
