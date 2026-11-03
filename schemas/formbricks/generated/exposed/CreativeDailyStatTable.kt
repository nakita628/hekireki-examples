package formbricks.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object CreativeDailyStatTable : CompositeIdTable("\"CreativeDailyStat\"") {
    val creativeId =
        reference(
            "creativeId",
            CreativeTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CreativeDailyStat_creativeId_fkey\"",
        )
    val date = pgDate("date").entityId()
    val deviceType = pgEnum("deviceType", "\"CreativeDeviceType\"", CreativeDeviceType.entries, CreativeDeviceType::dbName).entityId()
    val impressions = integer("impressions").default(0)
    val clicks = integer("clicks").default(0)
    override val primaryKey = PrimaryKey(creativeId, date, deviceType, name = "\"CreativeDailyStat_pkey\"")

    init {
        addIdColumn(creativeId)
    }
}
