package calcom.models

import org.jetbrains.exposed.v1.core.Column
import org.jetbrains.exposed.v1.core.ColumnType
import org.jetbrains.exposed.v1.core.Function
import org.jetbrains.exposed.v1.core.IColumnType
import org.jetbrains.exposed.v1.core.QueryBuilder
import org.jetbrains.exposed.v1.core.Table
import org.jetbrains.exposed.v1.core.statements.api.RowApi
import org.jetbrains.exposed.v1.dao.Entity
import java.security.SecureRandom
import java.time.Instant
import java.time.LocalDate
import java.time.LocalDateTime
import java.time.LocalTime
import java.time.ZoneOffset
import java.time.temporal.ChronoUnit
import java.util.UUID

internal class SqlExpression<T>(
    private val sql: String,
    columnType: IColumnType<T & Any>,
) : Function<T>(columnType) {
    override fun toQueryBuilder(queryBuilder: QueryBuilder) {
        queryBuilder.append(sql)
    }
}

internal fun <T> Column<T>.databaseDefault(
    sql: String,
    value: (() -> T)? = null,
): Column<T> =
    with(table) {
        if (value == null) defaultExpression(SqlExpression(sql, columnType)) else clientDefault(value).withDefinition("DEFAULT $sql")
    }

internal class PgTimestampColumnType(
    val precision: Int? = null,
) : ColumnType<Instant>() {
    override fun sqlType(): String = if (precision == null) "TIMESTAMP" else "TIMESTAMP($precision)"

    override fun valueFromDB(value: Any): Instant =
        when (value) {
            is Instant -> value
            is LocalDateTime -> value.toInstant(ZoneOffset.UTC)
            else -> error("Unexpected value of type ${value::class.qualifiedName}: $value")
        }.truncatedTo(ChronoUnit.MILLIS)

    override fun readObject(
        rs: RowApi,
        index: Int,
    ): Any? = rs.getObject(index, LocalDateTime::class.java)

    override fun notNullValueToDB(value: Instant): Any = LocalDateTime.ofInstant(value.truncatedTo(ChronoUnit.MILLIS), ZoneOffset.UTC)

    override fun nonNullValueToString(value: Instant): String = "'${notNullValueToDB(value)}'"
}

internal fun Table.pgTimestamp(
    name: String,
    precision: Int? = null,
): Column<Instant> = registerColumn(name, PgTimestampColumnType(precision))

internal class PgDateColumnType : ColumnType<LocalDate>() {
    override fun sqlType(): String = "DATE"

    override fun valueFromDB(value: Any): LocalDate =
        when (value) {
            is LocalDate -> value
            else -> error("Unexpected value of type ${value::class.qualifiedName}: $value")
        }

    override fun readObject(
        rs: RowApi,
        index: Int,
    ): Any? = rs.getObject(index, LocalDate::class.java)

    override fun nonNullValueToString(value: LocalDate): String = "'$value'"
}

internal fun Table.pgDate(name: String): Column<LocalDate> = registerColumn(name, PgDateColumnType())

internal class PgTimeColumnType(
    val precision: Int? = null,
) : ColumnType<LocalTime>() {
    override fun sqlType(): String = if (precision == null) "TIME" else "TIME($precision)"

    override fun valueFromDB(value: Any): LocalTime =
        when (value) {
            is LocalTime -> value
            else -> error("Unexpected value of type ${value::class.qualifiedName}: $value")
        }.truncatedTo(ChronoUnit.MILLIS)

    override fun readObject(
        rs: RowApi,
        index: Int,
    ): Any? = rs.getObject(index, LocalTime::class.java)

    override fun notNullValueToDB(value: LocalTime): Any = value.truncatedTo(ChronoUnit.MILLIS)

    override fun nonNullValueToString(value: LocalTime): String = "'${notNullValueToDB(value)}'"
}

internal fun Table.pgTime(
    name: String,
    precision: Int? = null,
): Column<LocalTime> = registerColumn(name, PgTimeColumnType(precision))

internal class PgEnumColumnType<E : Enum<E>>(
    val typeName: String,
    val entries: List<E>,
    val dbName: (E) -> String,
) : ColumnType<E>() {
    override fun sqlType(): String = typeName

    override fun valueFromDB(value: Any): E =
        entries.firstOrNull { it == value }
            ?: entries.firstOrNull { dbName(it) == value.toString() }
            ?: error("Unexpected value of $typeName: $value")

    override fun readObject(
        rs: RowApi,
        index: Int,
    ): Any? = rs.getString(index)

    override fun notNullValueToDB(value: E): Any = dbName(value)

    override fun nonNullValueToString(value: E): String = "'${dbName(value).replace("'", "''")}'"

    override fun parameterMarker(value: E?): String = "?::$typeName"
}

internal fun <E : Enum<E>> Table.pgEnum(
    name: String,
    typeName: String,
    entries: List<E>,
    dbName: (E) -> String,
): Column<E> = registerColumn(name, PgEnumColumnType(typeName, entries, dbName))

private val random = SecureRandom()

internal fun uuidV7(): UUID {
    val bytes = ByteArray(10).also(random::nextBytes)
    val randomA = ((bytes[0].toLong() and 0x0f) shl 8) or (bytes[1].toLong() and 0xff)
    val randomB = bytes.drop(2).fold(0L) { acc, byte -> (acc shl 8) or (byte.toLong() and 0xff) }
    return UUID((System.currentTimeMillis() shl 16) or 0x7000L or randomA, (randomB and 0x3fffffffffffffffL) or Long.MIN_VALUE)
}

internal fun Entity<*>.isWritten(column: Column<*>): Boolean = writeValues.keys.any { it == column }
