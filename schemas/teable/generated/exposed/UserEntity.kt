package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class UserEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserEntity>(UserTable)

    var name by UserTable.name
    var password by UserTable.password
    var salt by UserTable.salt
    var phone by UserTable.phone
    var email by UserTable.email
    var avatar by UserTable.avatar
    var isSystem by UserTable.isSystem
    var isAdmin by UserTable.isAdmin
    var isTrialUsed by UserTable.isTrialUsed
    var lang by UserTable.lang
    var notifyMeta by UserTable.notifyMeta
    var lastSignTime by UserTable.lastSignTime
    var deactivatedTime by UserTable.deactivatedTime
    var createdTime by UserTable.createdTime
    var deletedTime by UserTable.deletedTime
    var lastModifiedTime by UserTable.lastModifiedTime
    var permanentDeletedTime by UserTable.permanentDeletedTime
    var refMeta by UserTable.refMeta
    val accounts by AccountEntity referrersOn AccountTable.userId
    val baseOrders by UserBaseOrderEntity referrersOn UserBaseOrderTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
