package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraObservationalMemoryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraObservationalMemoryEntity>(MastraObservationalMemoryTable)

    var lookupKey by MastraObservationalMemoryTable.lookupKey
    var scope by MastraObservationalMemoryTable.scope
    var resourceId by MastraObservationalMemoryTable.resourceId
    var threadId by MastraObservationalMemoryTable.threadId
    var activeObservations by MastraObservationalMemoryTable.activeObservations
    var activeObservationsPendingUpdate by MastraObservationalMemoryTable.activeObservationsPendingUpdate
    var originType by MastraObservationalMemoryTable.originType
    var config by MastraObservationalMemoryTable.config
    var generationCount by MastraObservationalMemoryTable.generationCount
    var lastObservedAt by MastraObservationalMemoryTable.lastObservedAt
    var lastReflectionAt by MastraObservationalMemoryTable.lastReflectionAt
    var pendingMessageTokens by MastraObservationalMemoryTable.pendingMessageTokens
    var totalTokensObserved by MastraObservationalMemoryTable.totalTokensObserved
    var observationTokenCount by MastraObservationalMemoryTable.observationTokenCount
    var isObserving by MastraObservationalMemoryTable.isObserving
    var isReflecting by MastraObservationalMemoryTable.isReflecting
    var observedMessageIds by MastraObservationalMemoryTable.observedMessageIds
    var observedTimezone by MastraObservationalMemoryTable.observedTimezone
    var bufferedObservations by MastraObservationalMemoryTable.bufferedObservations
    var bufferedObservationTokens by MastraObservationalMemoryTable.bufferedObservationTokens
    var bufferedMessageIds by MastraObservationalMemoryTable.bufferedMessageIds
    var bufferedReflection by MastraObservationalMemoryTable.bufferedReflection
    var bufferedReflectionTokens by MastraObservationalMemoryTable.bufferedReflectionTokens
    var bufferedReflectionInputTokens by MastraObservationalMemoryTable.bufferedReflectionInputTokens
    var reflectedObservationLineCount by MastraObservationalMemoryTable.reflectedObservationLineCount
    var bufferedObservationChunks by MastraObservationalMemoryTable.bufferedObservationChunks
    var isBufferingObservation by MastraObservationalMemoryTable.isBufferingObservation
    var isBufferingReflection by MastraObservationalMemoryTable.isBufferingReflection
    var lastBufferedAtTokens by MastraObservationalMemoryTable.lastBufferedAtTokens
    var lastBufferedAtTime by MastraObservationalMemoryTable.lastBufferedAtTime
    var metadata by MastraObservationalMemoryTable.metadata
    var createdAt by MastraObservationalMemoryTable.createdAt
    var updatedAt by MastraObservationalMemoryTable.updatedAt
    var lastObservedAtZ by MastraObservationalMemoryTable.lastObservedAtZ
    var lastReflectionAtZ by MastraObservationalMemoryTable.lastReflectionAtZ
    var lastBufferedAtTimeZ by MastraObservationalMemoryTable.lastBufferedAtTimeZ
    var createdAtZ by MastraObservationalMemoryTable.createdAtZ
    var updatedAtZ by MastraObservationalMemoryTable.updatedAtZ
}
