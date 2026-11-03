package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object CouponTable : IdTable<String>("\"Coupon\"") {
    val userPropertiesToUpdate = jsonb<String>("userPropertiesToUpdate", { it }, { it })
    override val id = text("code").entityId()
    val dateRedeemed = pgTimestamp("dateRedeemed", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Coupon_pkey\"")

    init {
        uniqueIndex("\"Coupon_code_key\"", id)
    }
}
