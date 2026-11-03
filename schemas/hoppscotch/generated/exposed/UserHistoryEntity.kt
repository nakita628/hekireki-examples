package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class UserHistoryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserHistoryEntity>(UserHistoryTable)

    var userUid by UserHistoryTable.userUid
    var reqType by UserHistoryTable.reqType
    var request by UserHistoryTable.request
    var responseMetadata by UserHistoryTable.responseMetadata
    var isStarred by UserHistoryTable.isStarred
    var executedOn by UserHistoryTable.executedOn
    var user by UserEntity referencedOn UserHistoryTable.userUid
}
