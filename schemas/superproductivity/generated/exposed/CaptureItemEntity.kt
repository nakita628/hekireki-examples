package superproductivity.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class CaptureItemEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CaptureItemEntity>(CaptureItemTable)

    var userId by CaptureItemTable.userId
    var grantId by CaptureItemTable.grantId
    var ciphertext by CaptureItemTable.ciphertext
    var taskId by CaptureItemTable.taskId
    var createdAt by CaptureItemTable.createdAt
    var leaseDeviceId by CaptureItemTable.leaseDeviceId
    var leaseUntil by CaptureItemTable.leaseUntil
    var user by UserEntity referencedOn CaptureItemTable.userId
}
