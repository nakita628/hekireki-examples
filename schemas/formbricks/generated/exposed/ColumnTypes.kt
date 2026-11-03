package formbricks.models

import org.jetbrains.exposed.v1.core.ArrayColumnType
import org.jetbrains.exposed.v1.core.Column
import org.jetbrains.exposed.v1.core.ColumnType
import org.jetbrains.exposed.v1.core.Function
import org.jetbrains.exposed.v1.core.IColumnType
import org.jetbrains.exposed.v1.core.QueryBuilder
import org.jetbrains.exposed.v1.core.Table
import org.jetbrains.exposed.v1.core.statements.api.PreparedStatementApi
import org.jetbrains.exposed.v1.core.statements.api.RowApi
import org.jetbrains.exposed.v1.dao.Entity
import java.sql.ResultSet
import java.time.Instant
import java.time.LocalDate
import java.time.LocalDateTime
import java.time.ZoneOffset
import java.time.temporal.ChronoUnit
import java.sql.Array as SqlArray

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

internal class PgListColumnType<T : Any>(
    val element: ColumnType<T>,
    val elementClass: Class<*>,
) : ColumnType<List<T>>() {
    private val array = ArrayColumnType<T, List<T>>(element)

    override fun sqlType(): String = array.sqlType()

    override fun valueFromDB(value: Any): List<T> =
        when (value) {
            is SqlArray -> {
                value.resultSet.use { rows ->
                    buildList {
                        while (rows.next()) add(elementFromDB(read(rows)))
                    }
                }
            }

            is List<*> -> {
                value.map(::elementFromDB)
            }

            is Array<*> -> {
                value.map(::elementFromDB)
            }

            else -> {
                error("Unexpected value of type ${value::class.qualifiedName}: $value")
            }
        }

    private fun elementFromDB(value: Any?): T = value?.let(element::valueFromDB) ?: error("NULL in a list of ${element.sqlType()}")

    private fun read(rows: ResultSet): Any? = if (elementClass == String::class.java) rows.getString(2) else rows.getObject(2, elementClass)

    override fun notNullValueToDB(value: List<T>): Any = array.notNullValueToDB(value)

    override fun nonNullValueToString(value: List<T>): String = array.nonNullValueToString(value)

    override fun nonNullValueAsDefaultString(value: List<T>): String = array.nonNullValueAsDefaultString(value)

    override fun setParameter(
        stmt: PreparedStatementApi,
        index: Int,
        value: Any?,
    ) {
        array.setParameter(stmt, index, value)
    }
}

internal fun <T : Any> Table.pgList(
    name: String,
    element: ColumnType<T>,
    elementClass: Class<*>,
): Column<List<T>> = registerColumn(name, PgListColumnType(element, elementClass))

internal fun Entity<*>.isWritten(column: Column<*>): Boolean = writeValues.keys.any { it == column }
