package opensaas.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class UserEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserEntity>(UserTable)

    var createdAt by UserTable.createdAt
    var email by UserTable.email
    var username by UserTable.username
    var isAdmin by UserTable.isAdmin
    var paymentProcessorUserId by UserTable.paymentProcessorUserId
    var subscriptionStatus by UserTable.subscriptionStatus
    var subscriptionPlan by UserTable.subscriptionPlan
    var datePaid by UserTable.datePaid
    var credits by UserTable.credits
    val gptResponses by GptResponseEntity referrersOn GptResponseTable.userId
    val contactFormMessages by ContactFormMessageEntity referrersOn ContactFormMessageTable.userId
    val tasks by TaskEntity referrersOn TaskTable.userId
    val files by FileEntity referrersOn FileTable.userId
}
