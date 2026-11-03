defmodule Teable.Field do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          options: String.t() | nil,
          meta: String.t() | nil,
          ai_config: String.t() | nil,
          type: String.t(),
          cell_value_type: String.t(),
          is_multiple_cell_value: boolean() | nil,
          db_field_type: String.t(),
          db_field_name: String.t(),
          provision_state: atom(),
          not_null: boolean() | nil,
          unique: boolean() | nil,
          is_primary: boolean() | nil,
          is_computed: boolean() | nil,
          is_lookup: boolean() | nil,
          is_conditional_lookup: boolean() | nil,
          is_pending: boolean() | nil,
          has_error: boolean() | nil,
          lookup_linked_field_id: String.t() | nil,
          lookup_options: String.t() | nil,
          order: float(),
          version: integer(),
          created_time: DateTime.t(),
          deleted_time: DateTime.t() | nil,
          created_by: String.t(),
          last_modified_by: String.t() | nil,
          updated_at: DateTime.t() | nil,
          table: Teable.TableMeta.t() | nil
        }

  schema "field" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:description, :string)
    field(:options, :string)
    field(:meta, :string)
    field(:ai_config, :string)
    field(:type, :string)
    field(:cell_value_type, :string)
    field(:is_multiple_cell_value, :boolean)
    field(:db_field_type, :string)
    field(:db_field_name, :string)
    field(:provision_state, Ecto.Enum, values: [:pending, :ready, :error, :deleting], default: :ready)
    field(:not_null, :boolean)
    field(:unique, :boolean)
    field(:is_primary, :boolean)
    field(:is_computed, :boolean)
    field(:is_lookup, :boolean)
    field(:is_conditional_lookup, :boolean)
    field(:is_pending, :boolean)
    field(:has_error, :boolean)
    field(:lookup_linked_field_id, :string)
    field(:lookup_options, :string)
    field(:order, :float)
    field(:version, :integer)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:deleted_time, Teable.PrismaDateTime)
    field(:created_by, :string)
    field(:last_modified_by, :string)
    belongs_to(:table, Teable.TableMeta, foreign_key: :table_id, type: :string)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end