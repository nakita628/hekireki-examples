package superproductivity.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class UserSyncStateEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, UserSyncStateEntity>(UserSyncStateTable)

    var lastSeq by UserSyncStateTable.lastSeq
    var lastSnapshotSeq by UserSyncStateTable.lastSnapshotSeq
    var snapshotData by UserSyncStateTable.snapshotData
    var snapshotAt by UserSyncStateTable.snapshotAt
    var snapshotSchemaVersion by UserSyncStateTable.snapshotSchemaVersion
    var latestFullStateSeq by UserSyncStateTable.latestFullStateSeq
    var latestFullStateVectorClock by UserSyncStateTable.latestFullStateVectorClock
    var latestStateReplacementSeq by UserSyncStateTable.latestStateReplacementSeq
    val user by UserEntity referencedOn UserSyncStateTable.id
}
