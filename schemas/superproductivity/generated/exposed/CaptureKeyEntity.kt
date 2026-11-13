package superproductivity.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class CaptureKeyEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, CaptureKeyEntity>(CaptureKeyTable)

    var publicKey by CaptureKeyTable.publicKey
    var fingerprint by CaptureKeyTable.fingerprint
    var createdAt by CaptureKeyTable.createdAt
    var disabledAt by CaptureKeyTable.disabledAt
    val user by UserEntity referencedOn CaptureKeyTable.id
}
