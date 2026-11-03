defmodule Teable.SpaceDataDbMigrationJob do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          target_mode: String.t(),
          switch_on_completion: boolean(),
          state: atom(),
          target_url_fingerprint: String.t(),
          target_internal_schema: String.t(),
          inventory: map() | nil,
          copy_stats: map() | nil,
          validation_stats: map() | nil,
          last_error: String.t() | nil,
          started_at: DateTime.t() | nil,
          completed_at: DateTime.t() | nil,
          created_by: String.t(),
          created_time: DateTime.t(),
          updated_at: DateTime.t() | nil,
          space: Teable.Space.t() | nil,
          source_connection: Teable.DataDbConnection.t() | nil,
          target_connection: Teable.DataDbConnection.t() | nil
        }

  schema "space_data_db_migration_job" do
    field(:id, :string, primary_key: true)
    field(:target_mode, :string, default: "migrate-space")
    field(:switch_on_completion, :boolean, default: false)
    field(:state, Ecto.Enum, values: [:pending, :waiting_worker, :preflight, :freezing_writes, :copying, :validating, :switching, :succeeded, :failed, :canceled, :rolled_back], default: :pending)
    field(:target_url_fingerprint, :string)
    field(:target_internal_schema, :string)
    field(:inventory, :map)
    field(:copy_stats, :map)
    field(:validation_stats, :map)
    field(:last_error, :string)
    field(:started_at, Teable.PrismaDateTime)
    field(:completed_at, Teable.PrismaDateTime)
    field(:created_by, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    belongs_to(:space, Teable.Space, foreign_key: :space_id, type: :string)
    belongs_to(:source_connection, Teable.DataDbConnection, foreign_key: :source_connection_id, type: :string)
    belongs_to(:target_connection, Teable.DataDbConnection, foreign_key: :target_connection_id, type: :string)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end