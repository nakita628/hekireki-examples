package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ClaimableCustomPlanEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ClaimableCustomPlanEntity>(ClaimableCustomPlanTable)

    var createdAt by ClaimableCustomPlanTable.createdAt
    var claimedAt by ClaimableCustomPlanTable.claimedAt
    var name by ClaimableCustomPlanTable.name
    var description by ClaimableCustomPlanTable.description
    var price by ClaimableCustomPlanTable.price
    var currency by ClaimableCustomPlanTable.currency
    var workspaceId by ClaimableCustomPlanTable.workspaceId
    var workspace by WorkspaceEntity referencedOn ClaimableCustomPlanTable.workspaceId
    var chatsLimit by ClaimableCustomPlanTable.chatsLimit
    var storageLimit by ClaimableCustomPlanTable.storageLimit
    var seatsLimit by ClaimableCustomPlanTable.seatsLimit
    var isYearly by ClaimableCustomPlanTable.isYearly
    var companyName by ClaimableCustomPlanTable.companyName
    var vatType by ClaimableCustomPlanTable.vatType
    var vatValue by ClaimableCustomPlanTable.vatValue
}
