package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object ReverseSyncControlTable : CompositeIdTable("reverse_sync_control") {
    val workspaceId = text("workspace_id").entityId()
    val syncId = text("sync_id").entityId()
    val runId = text("run_id").entityId()
    val runOrder = long("run_order").default(0L)
    val detached = bool("detached").default(false)
    val revision = text("revision")
    val targetHash = text("target_hash")
    val phase = text("phase").default("new")
    val mode = pgEnum("mode", "\"reverse_sync_mode\"", ReverseSyncMode.entries, ReverseSyncMode::dbName)
    val extraction = pgEnum("extraction", "\"reverse_sync_extraction\"", ReverseSyncExtraction.entries, ReverseSyncExtraction::dbName)
    val artifactHead = binary("artifact_head").nullable()
    val baseSequence = long("base_sequence").default(0L)
    val nextSequence = long("next_sequence").default(0L)
    val checkpointSequence = long("checkpoint_sequence").default(0L)
    val finishSequence = long("finish_sequence").nullable()
    val store = binary("store").nullable()
    val providerState = binary("provider_state").nullable()
    val finishResult = binary("finish_result").nullable()
    val committedGeneration = text("committed_generation").nullable()
    override val primaryKey = PrimaryKey(workspaceId, syncId, runId, name = "reverse_sync_control_pkey")

    init {
        index("reverse_sync_control_sync_id_idx", false, syncId)
        uniqueIndex("reverse_sync_control_workspace_id_sync_id_run_order_key", workspaceId, syncId, runOrder)
    }
}
