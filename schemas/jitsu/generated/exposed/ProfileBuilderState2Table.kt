package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object ProfileBuilderState2Table : IdTable<String>("\"ProfileBuilderState2\"") {
    override val id = text("profileBuilderId").entityId()
    val updatedAt = pgTimestamp("updatedAt", 3)
    val fullRebuildInfo = jsonb<String>("fullRebuildInfo", { it }, { it }).nullable()
    val queuesInfo = jsonb<String>("queuesInfo", { it }, { it }).nullable()
    val metrics = jsonb<String>("metrics", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"ProfileBuilderState2_pkey\"")
}
