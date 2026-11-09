package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ReverseSyncTargetOwnerTable : IdTable<String>("reverse_sync_target_owner") {
    override val id = text("target_hash").entityId()
    val workspaceId = text("workspace_id")
    val syncId = text("sync_id")
    override val primaryKey = PrimaryKey(id, name = "reverse_sync_target_owner_pkey")

    init {
        uniqueIndex("reverse_sync_target_owner_workspace_id_sync_id_key", workspaceId, syncId)
    }
}
