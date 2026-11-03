package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object NotificationsSubscriptionsTable : IdTable<Int>("\"NotificationsSubscriptions\"") {
    override val id = integer("id").autoIncrement().entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"NotificationsSubscriptions_userId_fkey\"",
        )
    val subscription = text("subscription")
    override val primaryKey = PrimaryKey(id, name = "\"NotificationsSubscriptions_pkey\"")

    init {
        index("\"NotificationsSubscriptions_userId_subscription_idx\"", false, userId, subscription)
    }
}
