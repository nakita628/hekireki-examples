package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class CouponEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CouponEntity>(CouponTable)

    var userPropertiesToUpdate by CouponTable.userPropertiesToUpdate
    var dateRedeemed by CouponTable.dateRedeemed
}
