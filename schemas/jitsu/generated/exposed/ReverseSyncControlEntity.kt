package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class ReverseSyncControlEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<ReverseSyncControlEntity>(ReverseSyncControlTable)

    val workspaceId by ReverseSyncControlTable.workspaceId
    val syncId by ReverseSyncControlTable.syncId
    val runId by ReverseSyncControlTable.runId
    var runOrder by ReverseSyncControlTable.runOrder
    var detached by ReverseSyncControlTable.detached
    var revision by ReverseSyncControlTable.revision
    var targetHash by ReverseSyncControlTable.targetHash
    var phase by ReverseSyncControlTable.phase
    var mode by ReverseSyncControlTable.mode
    var extraction by ReverseSyncControlTable.extraction
    var artifactHead by ReverseSyncControlTable.artifactHead
    var baseSequence by ReverseSyncControlTable.baseSequence
    var nextSequence by ReverseSyncControlTable.nextSequence
    var checkpointSequence by ReverseSyncControlTable.checkpointSequence
    var finishSequence by ReverseSyncControlTable.finishSequence
    var store by ReverseSyncControlTable.store
    var providerState by ReverseSyncControlTable.providerState
    var finishResult by ReverseSyncControlTable.finishResult
    var committedGeneration by ReverseSyncControlTable.committedGeneration
}
