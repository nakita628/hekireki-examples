package superproductivity.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class SyncDeviceEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<SyncDeviceEntity>(SyncDeviceTable)

    val clientId by SyncDeviceTable.clientId
    val userId by SyncDeviceTable.userId
    var deviceName by SyncDeviceTable.deviceName
    var userAgent by SyncDeviceTable.userAgent
    var appVersion by SyncDeviceTable.appVersion
    var lastSeenAt by SyncDeviceTable.lastSeenAt
    var lastAckedSeq by SyncDeviceTable.lastAckedSeq
    var createdAt by SyncDeviceTable.createdAt
    val user by UserEntity referencedOn SyncDeviceTable.userId
}
